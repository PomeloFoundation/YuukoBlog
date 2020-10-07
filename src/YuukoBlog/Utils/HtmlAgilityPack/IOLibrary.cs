
using System.IO;

namespace Pomelo.HtmlAgilityPack
{
    internal struct IOLibrary
    {
        #region Internal Methods

        internal static void CopyAlways(string source, string target)
        {
            if (!File.Exists(source))
                return;
            Directory.CreateDirectory(Path.GetDirectoryName(target));
            MakeWritable(target);
            File.Copy(source, target, true);
        }
#if !PocketPC && !WINDOWS_PHONE
        internal static void MakeWritable(string path)
        {
            if (!File.Exists(path))
                return;
            File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);
        }
#else
		internal static void MakeWritable(string path)
        {
        }
#endif
        #endregion
    }
}