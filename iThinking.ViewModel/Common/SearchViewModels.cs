namespace iThinking.ViewModel.Common
{
    public class SearchViewModel
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string Keyword { set; get; }

        public SearchViewModel()
        {
            PageSize = 10;
        }
    }
}