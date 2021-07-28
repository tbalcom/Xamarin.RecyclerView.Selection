namespace AndroidXRecyclerViewSelection
{
    using AndroidX.RecyclerView.Selection;
    using AndroidX.RecyclerView.Widget;
    using System;

    public class ZipKeyProvider : ItemKeyProvider
    {
        private readonly RecyclerView recyclerView;

        public ZipKeyProvider(RecyclerView recyclerView) : base(ItemKeyProvider.ScopeMapped)
        {
            this.recyclerView = recyclerView;
        }

        public override Java.Lang.Object GetKey(int p0)
        {
            return recyclerView.GetAdapter().GetItemId(p0);
        }

        public override int GetPosition(Java.Lang.Object p0)
        {
            var viewHolder = recyclerView.FindViewHolderForItemId(Convert.ToInt64(p0));
            return viewHolder?.LayoutPosition ?? RecyclerView.NoPosition;
        }
    }
}