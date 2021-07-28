namespace AndroidXRecyclerViewSelection
{
    using AndroidX.RecyclerView.Selection;

    public class CarItemDetails : ItemDetailsLookup.ItemDetails
    {
        private readonly int position;
        private readonly Java.Lang.Object key;

        public CarItemDetails(int position, Java.Lang.Object key)
        {
            this.position = position;
            this.key = key;
        }

        public override int Position => position;

        protected override Java.Lang.Object RawSelectionKey => key;
    }
}
