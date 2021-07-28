namespace AndroidXRecyclerViewSelection
{
    using AndroidX.RecyclerView.Selection;

    public class ZipItemDetails : ItemDetailsLookup.ItemDetails
    {
        private readonly int adapterPosition;
        private readonly long selectionKey;

        public ZipItemDetails(int adapterPosition, long selectionKey)
        {
            this.adapterPosition = adapterPosition;
            this.selectionKey = selectionKey;
        }

        public override int Position => adapterPosition;

        protected override Java.Lang.Object RawSelectionKey => selectionKey;
    }
}