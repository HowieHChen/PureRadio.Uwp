using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.QingTing.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Adapters
{
    public class SearchAdapter : ISearchAdapter
    {
        public string ConvertToSearchSuggest(SearchSuggestItem item)
            => item.Title;

        public RadioInfoSearch ConvertToSearchResultView(SearchRadioItem item)
            => new(item.RadioId, item.Title, item.Cover, item.Description, item.AudienceCount);


        public ContentInfoSearch ConvertToSearchResultView(SearchContentItem item)
            => new(item.ContentId, item.Title, item.Podcaster, item.Cover, item.Description, item.PlayCount);
    }
}
