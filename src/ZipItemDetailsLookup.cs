namespace AndroidXRecyclerViewSelection
{
    using Android.Views;
    using AndroidX.RecyclerView.Selection;
    using AndroidX.RecyclerView.Widget;

    public class ZipItemDetailsLookup : ItemDetailsLookup
    {
        private readonly RecyclerView recyclerView;

        public ZipItemDetailsLookup(RecyclerView recyclerView)
        {
            this.recyclerView = recyclerView;
        }

        public override ItemDetails GetItemDetails(MotionEvent p0)
        {
            var view = recyclerView.FindChildViewUnder(p0.GetX(), p0.GetY());
            if (view != null)
            {
                var viewHolder = recyclerView.GetChildViewHolder(view);
                if (viewHolder is ZipViewHolder zipViewHolder)
                {
                    return zipViewHolder.GetItemDetails();
                }
            }
            return null;
        }
    }
}