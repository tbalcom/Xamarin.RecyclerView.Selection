namespace AndroidXRecyclerViewSelection
{
    using Android.Views;
    using AndroidX.RecyclerView.Selection;
    using AndroidX.RecyclerView.Widget;

    public class CarItemDetailsLookup : ItemDetailsLookup
    {
        private readonly RecyclerView recyclerView;

        public CarItemDetailsLookup(RecyclerView recyclerView)
        {
            this.recyclerView = recyclerView;
        }

        public override ItemDetails GetItemDetails(MotionEvent @event)
        {
            var view = recyclerView.FindChildViewUnder(@event.GetX(), @event.GetY());
            if (view != null)
            {
                var viewHolder = recyclerView.GetChildViewHolder(view);
                if (viewHolder is CarAdapter.CarViewHolder cvh)
                {
                    return cvh.GetItemDetails();
                }
            }
            return null;
        }
    }
}
