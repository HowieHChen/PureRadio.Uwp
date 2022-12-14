using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Models.QingTing.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace PureRadio.Uwp.Adapters
{
    public class ContentAdapter : IContentAdapter
    {
        public ContentInfoDetail ConvertToContentInfoDetail(ContentDetailItem item, List<AttributesItem> attributes = null)
        {

            var resourceLoader = new ResourceLoader();
            string podcasters = resourceLoader.GetString("LangPodcasterUnknown");
            if (item.Podcasters != null && item.Podcasters.Count > 0)
            {
                var podcastersList = item.Podcasters?.Select(p => $"{p.Name}").ToList();
                podcastersList.Sort();
                podcasters = string.Join(',', podcastersList);
            }
            return new ContentInfoDetail(
                new Uri(item.Cover), item.ContentId, item.Version, item.Title, 
                item.Description, item.ProgramCount, item.PlayCount, (float)item.Rating / 2, 
                podcasters, item.CategoryId, item.ContentType, attributes);
        }


        public ContentPlaylistDetail ConvertToContentPlaylistItem(ContentProgramListItem item, string version, bool isContentFree)
            => new ContentPlaylistDetail(
                version, item.ProgramId, item.Title, item.Duration, item.UpdateTime,
                item.Sequence, isContentFree || item.IsFree, new Uri(item.Cover), item.ContentType, item.PlayCount);

        public ContentInfoCategory ConvertToContentInfoCategory(ContentCategoryItem item)
            => new ContentInfoCategory(
                item.ContentId, item.Title, item.Description,
                item.Cover, (float)item.Rating / 2, item.PlayCount);

        public ContentInfoRecommend ConvertToContentInfoRecommend(ContentRecommendItem item)
        {
            if (!int.TryParse(item.Link.ContentId, out int contentId))
                contentId = 0;
            return new ContentInfoRecommend(
                item.Cover, item.Title, item.RecWords, item.PlayCount, (float)item.Score / 2, contentId);
        }

        public ContentRecommendSet ConvertToContentRecommendSet(ContentCategoriesItem item)
        {
            var items = item.CategoryItems.Select(p => ConvertToContentInfoRecommend(p)).ToList();
            return new ContentRecommendSet(item.CategoryId, item.CategoryTittle, items);
        }
    }
}
