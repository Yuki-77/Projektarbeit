using Android.Content;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using System;

namespace xamarin_app.Droid
{
    public class NonSwipeableViewPager : ViewPager
    {

        private bool isEnabled;
        public enum SwipeDirection
        {
            all, left, right, none
        }
        private float initialXValue;
        private SwipeDirection direction = SwipeDirection.right;

        public NonSwipeableViewPager(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public NonSwipeableViewPager(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            this.isEnabled = true;
        }


        public override bool OnTouchEvent(MotionEvent e)
        {
            if (this.isEnabled && this.IsSwipeAllowed(e))
            {
                return base.OnTouchEvent(e);
            }
            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (this.isEnabled && this.IsSwipeAllowed(ev))
            {
                return base.OnInterceptTouchEvent(ev);
            }
            return false;
        }

        public void SetPagingEnabled(bool enabled)
        {
            this.isEnabled = enabled;
        }

        private bool IsSwipeAllowed(MotionEvent e)
        {
            if (this.direction == SwipeDirection.all) return true;

            if (direction == SwipeDirection.none) //disable any swipe
                return false;

            if (e.Action == MotionEventActions.Down)
            {
                initialXValue = e.GetX();
                return true;
            }

            if (e.Action == MotionEventActions.Move)
            {
                try
                {
                    float diffX = e.GetX() - initialXValue;
                    if (diffX > 0 && direction == SwipeDirection.right)
                    {
                        // swipe from left to right detected
                        return false;
                    }
                    else if (diffX < 0 && direction == SwipeDirection.left)
                    {
                        // swipe from right to left detected
                        return false;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.StackTrace);
                }
            }

            return true;
        }

        public void SetAllowedSwipeDirection(SwipeDirection direction)
        {
            this.direction = direction;
        }
    }
}