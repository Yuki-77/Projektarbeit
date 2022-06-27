using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using xamarin_app.Logic;

namespace xamarin_app.Droid
{
    public class ChooseAdapter : BaseAdapter<Choose>
    {
        readonly List<Choose> items;
        readonly Activity context;
        public ChooseAdapter(Activity context, List<Choose> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Choose this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.B3_category_item, null);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = item.GetName();
            int resourceId = (int)typeof(Resource.Drawable).GetField(item.GetIcon()).GetValue(null);
            view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(resourceId);
            if (item.GetExec())
            {
                view.FindViewById<ImageView>(Resource.Id.categoryStatusImg).SetImageResource(Resource.Drawable.completedCircle);
            }
            else
            {
                view.FindViewById<ImageView>(Resource.Id.categoryStatusImg).SetImageDrawable(null);
            }


            return view;
        }
    }
}