namespace ConsoleApp2
{
    /// <summary>
    /// Static class for storing Resources.
    /// </summary>
    public static class Pool
    {
        /// <summary>
        /// Resources of all the audio in the project.
        /// each index contain 2d array in size[2,N]
        /// where N is the amount of samples and 2 is the amount of channels.
        /// </summary>
        public static List<double[,]> Audio = new List<double[,]>();
    }
}
