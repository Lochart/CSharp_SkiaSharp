using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

/*
 * ССылка https://github.com/peterloos/Xamarin_ConnectFour/blob/master/ConnectFour/ConnectFour/MainPage.xaml
 */

namespace CSharp_SkiaSharp
{
    enum Stone { Empty, Black, White };

    enum GameStatus
    {
        WhiteIsNext,
        BlackIsNext,
        WhiteHasWon,
        BlackHasWon,
        EvenScore
    }

    struct BoardLocation
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }

    public class Game_Connect_Four : ContentPage
    {
        private const int MaxRows = 6;
        private const int MaxCols = 7;

        private const int BallMargin = 20;
        private const int BallStrokeWidth = 20;

        private GameStatus status;
        private Stone[,] board;
        private int moves;
        private Label labelStatus;
        private Button buttonClear;
        private SKCanvasView canvasConnectFour;
        private SKPaint singlePaint;
        private SKPoint[,] centerCoordinates;
        private int surfaceWidth;
        private int surfaceHeight;
        private int radius;

        public Game_Connect_Four()
        {
            var view = new StackLayout();
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            view.VerticalOptions = LayoutOptions.FillAndExpand;

            var header = new Label {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 32,
                Text = "Another Connect Four"
            };

            this.labelStatus = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 20,
                Text = ""
            };

            this.canvasConnectFour = new SKCanvasView();
            canvasConnectFour.HorizontalOptions = LayoutOptions.FillAndExpand;
            canvasConnectFour.VerticalOptions = LayoutOptions.FillAndExpand;
            canvasConnectFour.EnableTouchEvents = true;
            canvasConnectFour.PaintSurface += OnPaintSurface;
            canvasConnectFour.Touch += CanvasBoardTouched;
            canvasConnectFour.BackgroundColor = Color.Gray;

            this.buttonClear = new Button { 
                Text = "Reset",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill
            };
            buttonClear.Clicked += ButtonClicked;

            view.Children.Add(header);
            view.Children.Add(labelStatus);
            view.Children.Add(canvasConnectFour);
            view.Children.Add(buttonClear);

            this.InitGame();
            this.ResetGame();

            Content = view;
        }

        private void InitGame()
        {
            this.radius = -1;
            this.board = new Stone[MaxRows, MaxCols];
            this.singlePaint = new SKPaint();
            this.centerCoordinates = new SKPoint[MaxRows, MaxCols];
        }

        private void ResetGame()
        {
            this.status = GameStatus.WhiteIsNext;
            this.labelStatus.Text = "1. Player starts ...";
            this.labelStatus.BackgroundColor = Color.White;
            this.moves = 0;
            this.ResetBoard();
        }

        // event handling methods
        private void ButtonClicked(Object sender, EventArgs e)
        {
            this.ResetGame();
            this.canvasConnectFour.InvalidateSurface();
        }

        private void OnPaintSurface(Object sender, SKPaintSurfaceEventArgs args)
        {
            if (this.radius == -1)
                this.CalculateShapeMeasurements(args);

            using (SKCanvas canvas = args.Surface.Canvas)
            {
                this.DrawBoard(canvas);
            }
        }

        // private board helper methods
        private void CalculateShapeMeasurements(SKPaintSurfaceEventArgs args)
        {
            this.surfaceWidth = args.Info.Width;
            this.surfaceHeight = args.Info.Height;

            float rawRadius = this.surfaceWidth / (2 * MaxCols);
            this.radius = (int)(rawRadius - BallMargin);

            float distanceFromTop = (this.surfaceHeight - (MaxRows * 2 * rawRadius)) / 2;

            for (int i = 0; i < MaxRows; i++)
                for (int j = 0; j < MaxCols; j++)
                {
                    float centerX = (2 * j + 1) * rawRadius;
                    float centerY = distanceFromTop + (2 * i + 1) * rawRadius;

                    this.centerCoordinates[i, j] = new SKPoint(centerX, centerY);
                }
        }

        private void ResetBoard()
        {
            for (int i = 0; i < MaxRows; i++)
                for (int j = 0; j < MaxCols; j++)
                    this.board[i, j] = Stone.Empty;
        }

        // private drawing helper methods (valid SKCanvas object needed)
        private void DrawBoard(SKCanvas canvas)
        {
            for (int i = 0; i < MaxRows; i++)
                for (int j = 0; j < MaxCols; j++)
                {
                    Stone s = this.board[MaxRows - i - 1, j];

                    SKColor color =
                        (s == Stone.White) ? SKColors.Green :
                        (s == Stone.Black) ? SKColors.Red : SKColors.LightBlue;

                    this.DrawBall(canvas, i, j, this.singlePaint, color);
                }
        }

        private void DrawBall(SKCanvas canvas, int row, int col, SKPaint paint, SKColor color)
        {
            float centerX = this.centerCoordinates[row, col].X;
            float centerY = this.centerCoordinates[row, col].Y;

            paint.Style = SKPaintStyle.Stroke;
            paint.Color = SKColors.Black;
            paint.StrokeWidth = BallStrokeWidth;
            canvas.DrawCircle(centerX, centerY, this.radius, paint);

            paint.Style = SKPaintStyle.Fill;
            paint.Color = color;
            canvas.DrawCircle(centerX, centerY, this.radius, paint);
        }

        private void CanvasBoardTouched(Object sender, SKTouchEventArgs e)
        {
            BoardLocation cell = this.CalcBoardLocation(e.Location);

            if (!this.IsValidBoardRange(cell))
            {
                DisplayAlert("Touch", "Wrong location !", "Continue");
                return;
            }

            if (!this.IsValidBoardLocation(cell))
            {
                DisplayAlert("Touch", "Wrong position !", "Continue");
                return;
            }

            this.board[cell.Row, cell.Col] =
                (this.status == GameStatus.WhiteIsNext) ?
                Stone.White : Stone.Black;

            this.moves++;
            if (this.moves == MaxRows * MaxCols)
                this.status = GameStatus.EvenScore;
            else if (this.AreThereFourInARow(cell.Row, cell.Col, this.board[cell.Row, cell.Col]))
            {
                this.status = (this.status == GameStatus.WhiteIsNext) ?
                    GameStatus.WhiteHasWon : GameStatus.BlackHasWon;
            }
            else
            {
                this.status = (this.status == GameStatus.WhiteIsNext) ?
                GameStatus.BlackIsNext : GameStatus.WhiteIsNext;
            }

            this.UpdateUserInterface();

            this.canvasConnectFour.InvalidateSurface();
        }

        private BoardLocation CalcBoardLocation(SKPoint currentLocation)
        {
            float rawRadius = this.surfaceWidth / (2 * MaxCols);

            float distanceFromTop = (this.surfaceHeight - (MaxRows * 2 * rawRadius)) / 2;


            int col = (int)(currentLocation.X / (2 * rawRadius));
            int row = (int)((currentLocation.Y - distanceFromTop) / (2 * rawRadius));

            return new BoardLocation() { Row = MaxRows - row - 1, Col = col };
        }

        private bool IsValidBoardRange(BoardLocation cell)
        {
            return cell.Col >= 0 && cell.Col < MaxCols && cell.Row >= 0 && cell.Row < MaxRows;
        }

        private bool IsValidBoardLocation(BoardLocation cell)
        {
            if (cell.Row < 0 || cell.Row >= MaxRows)
                return false;
            if (cell.Col < 0 || cell.Col >= MaxCols)
                return false;

            if (this.board[cell.Row, cell.Col] != Stone.Empty)
                return false;

            for (int i = 0; i < cell.Row; i++)
                if (this.board[i, cell.Col] == Stone.Empty)
                    return false;

            if (this.status == GameStatus.WhiteHasWon ||
                this.status == GameStatus.BlackHasWon ||
                this.status == GameStatus.EvenScore)
                return false;

            return true;
        }

        private void UpdateUserInterface()
        {
            switch (this.status)
            {
                case GameStatus.WhiteIsNext:
                    this.labelStatus.Text = "1. Player is next ...";
                    break;

                case GameStatus.BlackIsNext:
                    this.labelStatus.Text = "2. Player is next ...";
                    break;

                case GameStatus.WhiteHasWon:
                    this.labelStatus.BackgroundColor = Color.Yellow;
                    this.labelStatus.Text = "Game over: 1. Player has won !";
                    break;

                case GameStatus.BlackHasWon:
                    this.labelStatus.BackgroundColor = Color.Yellow;
                    this.labelStatus.Text = "Game over: 2. Player has won !";
                    break;

                case GameStatus.EvenScore:
                    this.labelStatus.BackgroundColor = Color.LightBlue;
                    this.labelStatus.Text = "Game ended in a draw !";
                    break;
            }
        }

        private bool AreThereFourInARow(int row, int col, Stone lastStone)
        {
            int count = 0;
            for (int i = 0; i < MaxRows; i++)
                if (this.board[i, col] == lastStone)
                {
                    count++;
                    if (count == 4)
                        return true;
                }
                else
                    count = 0;

            count = 0;
            for (int j = 0; j < MaxCols; j++)
                if (this.board[row, j] == lastStone)
                {
                    count++;
                    if (count == 4)
                        return true;
                }
                else
                    count = 0;

            int delta1 = Math.Min(row, col);
            int delta2 = Math.Min(MaxRows - row, MaxCols - col);

            int row1 = row - delta1;
            int col1 = col - delta1;
            int row2 = row + delta2;
            int col2 = col + delta2;

            count = 0;
            for (int i = row1, j = col1; i < row2 && j < col2; i++, j++)
                if (this.board[i, j] == lastStone)
                {
                    count++;
                    if (count == 4)
                        return true;
                }
                else
                    count = 0;

            delta1 = Math.Min(MaxRows - row - 1, col);
            delta2 = Math.Min(row, MaxCols - col - 1);

            row1 = row + delta1;
            col1 = col - delta1;
            row2 = row - delta2;
            col2 = col + delta2;

            count = 0;
            for (int i = row1, j = col1; i >= row2 && j <= col2; i--, j++)
                if (this.board[i, j] == lastStone)
                {
                    count++;
                    if (count == 4)
                        return true;
                }
                else
                    count = 0;

            return false;
        }
    }
}

