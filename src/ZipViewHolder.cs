using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;

namespace AndroidXRecyclerViewSelection
{
    public class ZipViewHolder : RecyclerView.ViewHolder
    {
        private readonly TextView textView;

        public ZipViewHolder(View view) : base(view)
        {
            textView = view.FindViewById<TextView>(Resource.Id.zip_text);
        }

        public void Bind(int zip)
        {
            textView.Text = zip.ToString();
        }

        public ZipItemDetailsLookup.ItemDetails GetItemDetails()
        {
            return new ZipItemDetails(AdapterPosition, Convert.ToInt64(textView.Text));
        }
    }
}