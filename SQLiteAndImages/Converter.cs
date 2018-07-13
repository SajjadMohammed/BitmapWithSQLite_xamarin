using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;

using System.IO;
using System.Threading.Tasks;

using static Android.Graphics.Bitmap;

namespace SQLiteAndImages
{
    // this class is for converting bytes to bitmap and vice versa
    internal class Converter
    {
        Context context;
        public Converter(Context context)
        {
            this.context = context;
        }

        public async Task<byte[]> ImageToBytesAsync(ImageView imageView)
        {
            var bmp = (context.GetDrawable(Resource.Drawable.myImage) as BitmapDrawable).Bitmap;
            using (var memoryStream = new MemoryStream())
            {
                await bmp.CompressAsync(CompressFormat.Jpeg, 90, memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<Bitmap> BytesToImageAsync(byte[] bytes)
        {
            return await BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length);
        }
    }
}