using System;
using System.Drawing;

namespace PaquetVirtuel
{
    public class ImageCarte : IDisposable
    {
        public const int LARGEUR_CARTE = 349;
        public const int HAUTEUR_CARTE = 507;

        public Bitmap FeuilleSpriteCartes { get; private set; }
        public Point OrigineRecto { get; private set; }

        private readonly Point origineVerso;
        private Bitmap bitmapRecto;
        private static Bitmap bitmapVersoPartage;

        public ImageCarte(Bitmap feuilleSprite, Point origine)
        {
            FeuilleSpriteCartes = feuilleSprite ?? throw new ArgumentNullException(nameof(feuilleSprite));
            OrigineRecto = origine;
            origineVerso = new Point(700, FeuilleSpriteCartes.Height - HAUTEUR_CARTE);
        }

        public Bitmap ObtenirRecto()
        {
            if (bitmapRecto == null)
                bitmapRecto = ExtraireZone(OrigineRecto);
            return bitmapRecto;
        }

        public Bitmap ObtenirVerso()
        {
            if (bitmapVersoPartage == null)
                bitmapVersoPartage = ExtraireZone(origineVerso);
            return bitmapVersoPartage;
        }

        private Bitmap ExtraireZone(Point origine)
        {
            var rectangle = new Rectangle(origine.X, origine.Y, LARGEUR_CARTE, HAUTEUR_CARTE);

            if (rectangle.Right > FeuilleSpriteCartes.Width || rectangle.Bottom > FeuilleSpriteCartes.Height)
                throw new InvalidOperationException($"Zone {rectangle} dépasse les dimensions du sprite ({FeuilleSpriteCartes.Width}x{FeuilleSpriteCartes.Height}).");

            return FeuilleSpriteCartes.Clone(rectangle, FeuilleSpriteCartes.PixelFormat);
        }

        public void Dispose()
        {
            bitmapRecto?.Dispose();
        }
    }
}
