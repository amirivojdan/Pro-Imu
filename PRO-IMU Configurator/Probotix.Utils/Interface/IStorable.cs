namespace Probotix.Utils.Interface
{
    public interface IStorable
    {
        /// <summary>
        /// Save configuarations
        /// </summary>
        /// <param name="path">where to save configurations</param>
        void Save(string path);

        /// <summary>
        /// Save configuarations
        /// </summary>
        /// <param name="path">where to load configurations from</param>
        void Load(string path);
    }
}
