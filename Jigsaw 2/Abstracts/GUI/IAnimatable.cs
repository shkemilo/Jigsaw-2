namespace Jigsaw_2.Abstracts
{
    /// <summary>
    /// Extension for Animated GUI elements.
    /// </summary>
    public interface IAnimatable
    {
        /// <summary> Animates the GUI element. </summary>
        void Animate();

        void AnimationStop();
    }
}