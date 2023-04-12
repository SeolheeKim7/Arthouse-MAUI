using Arthouse_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arthouse_MAUI.Data
{
    internal interface IArtworkRepository
    {
        Task<List<Artwork>> GetArtworks();
        Task<Artwork> GetArtwork(int ID);
        Task<List<Artwork>> GetArtworksByArtType(int ArtTypeID);
        Task AddArtwork(Artwork artworkToAdd);
        Task UpdateArtwork(Artwork artworkToUpdate);
        Task DeleteArtwork(Artwork artworkToDelete);
    }
}
