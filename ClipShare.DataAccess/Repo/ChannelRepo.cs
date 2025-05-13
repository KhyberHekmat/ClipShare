using ClipShare.Core.Entities;
using ClipShare.Core.IRepo;
using ClipShare.DataAccess.Data;

namespace ClipShare.DataAccess.Repo
{
    public class ChannelRepo:BaseRepo<Channel>, IChannelRepo
    {
        public ChannelRepo(Context context):base(context)
        {
            
        }
    }
}
