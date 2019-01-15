using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using static Android.Support.V7.Widget.RecyclerView;

namespace SpotyPie.Helpers
{
    public interface IFastScrollRecyclerViewAdapter
    {
        Dictionary<string, int> GetMapIndex();
    }

    public class FastScrollRecyclerView : RecyclerView
    {
        public const int INDWIDTH = 25;
        public const int INDHEIGHT = 18;
        public float ScaledWidth { get; set; }
        public float ScaledHeight { get; set; }
        public string[] Sections { get; set; }
        public float Sx { get; set; }
        public float Sy { get; set; }
        public string Section { get; set; }
        public bool ShowLetter { get; set; }

        private ListHandler _listHandler;
        private bool _setupThings = false;
        private Context _context;

        public FastScrollRecyclerView(Context context) : base(context)
        {
            _context = context;
        }

        public FastScrollRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            _context = context;
        }

        public FastScrollRecyclerView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            _context = context;
        }

        public override void OnDraw(Canvas c)
        {
            if (!_setupThings && GetAdapter() != null)
                SetupThings();
            base.OnDraw(c);
        }

        private void SetupThings()
        {
            //create az text data
            var sectionSet = ((IFastScrollRecyclerViewAdapter)GetAdapter()).GetMapIndex().Keys;
            var listSection = new List<string>(sectionSet);
            listSection.Sort();
            Sections = new string[listSection.Count];
            int i = 0;
            foreach (var s in listSection)
            {
                Sections[i++] = s;
            }

            ScaledWidth = INDWIDTH * _context.Resources.DisplayMetrics.Density;
            var divisor = sectionSet.Count == 0 ? 1 : sectionSet.Count;
            ScaledHeight = Height / divisor;// INDHEIGHT * _context.Resources.DisplayMetrics.Density;
            Sx = Width - PaddingRight - (float)(1.2 * ScaledWidth);
            Sy = (float)((Height - (ScaledHeight * Sections.Length)) / 2.0);
            _setupThings = true;
        }

        public override bool OnTouchEvent(MotionEvent motionEvent)
        {
            if (_setupThings)
            {
                var adapter = GetAdapter() as IFastScrollRecyclerViewAdapter;

                var x = motionEvent.GetX();
                var y = motionEvent.GetY();

                switch (motionEvent.Action)
                {
                    case MotionEventActions.Down:
                        {
                            if (x < Sx - ScaledWidth || y < Sy || y > (Sy + ScaledHeight * Sections.Length))
                            {
                                return base.OnTouchEvent(motionEvent);
                            }
                            else
                            {
                                //// We touched the index bar
                                float yy = y - PaddingTop - PaddingBottom - Sy;
                                int currentPosition = (int)Math.Floor(yy / ScaledHeight);
                                if (currentPosition < 0) currentPosition = 0;
                                if (currentPosition >= Sections.Length) currentPosition = Sections.Length - 1;
                                Section = Sections[currentPosition];
                                ShowLetter = true;
                                int positionInData = 0;
                                if (adapter.GetMapIndex().ContainsKey(Section.ToUpper()))
                                {
                                    positionInData = adapter.GetMapIndex()[Section.ToUpper()];
                                }

                                (GetLayoutManager() as LinearLayoutManager).ScrollToPositionWithOffset(positionInData, 20);
                                Invalidate();
                            }
                            break;
                        }
                    case MotionEventActions.Move:
                        {
                            if (!ShowLetter && (x < Sx - ScaledWidth || y < Sy || y > (Sy + ScaledHeight * Sections.Length)))
                            {
                                return base.OnTouchEvent(motionEvent);
                            }
                            else
                            {
                                float yy = y - Sy;
                                int currentPosition = (int)Math.Floor(yy / ScaledHeight);
                                if (currentPosition < 0) currentPosition = 0;
                                if (currentPosition >= Sections.Length) currentPosition = Sections.Length - 1;
                                Section = Sections[currentPosition];
                                ShowLetter = true;
                                int positionInData = 0;
                                if (adapter.GetMapIndex().ContainsKey(Section.ToUpper()))
                                    positionInData = adapter.GetMapIndex()[Section.ToUpper()];
                                (GetLayoutManager() as LinearLayoutManager).ScrollToPositionWithOffset(positionInData, 20);
                                Invalidate();
                            }
                            break;
                        }
                    case MotionEventActions.Up:
                        {
                            _listHandler = new ListHandler(this);
                            _listHandler.DelayClear();
                            if (x < Sx - ScaledWidth || y < Sy || y > (Sy + ScaledHeight * Sections.Length))
                            {
                                return base.OnTouchEvent(motionEvent);
                            }
                            else
                            {
                                return true;
                            }
                        }
                }
            }

            return true;
        }

        private class ListHandler
        {
            FastScrollRecyclerView _parent;
            public ListHandler(FastScrollRecyclerView parent)
            {
                _parent = parent;
            }

            public async void DelayClear()
            {
                await Task.Delay(100);
                _parent.ShowLetter = false;
                _parent.Invalidate();
            }
        }
    }

    public class FastScrollRecyclerViewItemDecoration : ItemDecoration
    {
        private Context _context;
        public FastScrollRecyclerViewItemDecoration(Context context)
        {
            _context = context;
        }

        public override void OnDrawOver(Canvas canvas, RecyclerView parent, State state)
        {
            base.OnDrawOver(canvas, parent, state);

            float scaledWidth = ((FastScrollRecyclerView)parent).ScaledWidth;
            float sx = ((FastScrollRecyclerView)parent).Sx;
            float scaledHeight = ((FastScrollRecyclerView)parent).ScaledHeight;
            float sy = ((FastScrollRecyclerView)parent).Sy;
            string[] sections = ((FastScrollRecyclerView)parent).Sections;
            string section = ((FastScrollRecyclerView)parent).Section;
            bool showLetter = ((FastScrollRecyclerView)parent).ShowLetter;

            // We draw the letter in the middle
            if (showLetter & section != null && !section.Equals(""))
            {
                //overlay everything when displaying selected index Letter in the middle
                Paint overlayDark = new Paint();
                overlayDark.Color = Color.Black;
                overlayDark.Alpha = 100;
                canvas.DrawRect(0, 0, parent.Width, parent.Height, overlayDark);
                float middleTextSize = _context.Resources.GetDimension(Resource.Dimension.fast_scroll_overlay_text_size);
                Paint middleLetter = new Paint();
                middleLetter.Color = new Color(ContextCompat.GetColor(_context, Resource.Color.colorPrimary));
                middleLetter.TextSize = middleTextSize;
                middleLetter.AntiAlias = true;
                middleLetter.FakeBoldText = true;
                middleLetter.SetStyle(Paint.Style.Fill);
                int xPos = (canvas.Width - (int)middleTextSize) / 2;
                int yPos = (int)((canvas.Height / 2) - ((middleLetter.Descent() + middleLetter.Ascent()) / 2));


                canvas.DrawText(section.ToUpper(), xPos, yPos, middleLetter);
            }

            //        // draw indez A-Z

            Paint textPaint = new Paint();
            textPaint.AntiAlias = true;
            textPaint.SetStyle(Paint.Style.Fill);

            for (int i = 0; i < sections.Length; i++)
            {
                if (showLetter & section != null && !section.Equals("") && section != null
                        && sections[i].ToUpper().Equals(section.ToUpper()))
                {
                    textPaint.Color = Color.White;
                    textPaint.Alpha = 255;
                    textPaint.FakeBoldText = true;
                    textPaint.TextSize = scaledWidth / 2;
                    canvas.DrawText(sections[i].ToUpper(),
                            sx + textPaint.TextSize / 2, sy + parent.PaddingTop
                                    + scaledHeight * (i + 1), textPaint);
                    textPaint.TextSize = scaledWidth;
                    canvas.DrawText("•",
                            sx - textPaint.TextSize / 3, sy + parent.PaddingTop
                                    + scaledHeight * (i + 1) + scaledHeight / 3, textPaint);

                }
                else
                {
                    textPaint.Color = new Color(ContextCompat.GetColor(_context, Resource.Color.colorPrimary));
                    textPaint.Alpha = 200;
                    textPaint.FakeBoldText = false;
                    textPaint.TextSize = scaledWidth / 2;
                    canvas.DrawText(sections[i].ToUpper(),
                            sx + textPaint.TextSize / 2, sy + parent.PaddingTop
                                    + scaledHeight * (i + 1), textPaint);
                }
            }
        }
    }
}