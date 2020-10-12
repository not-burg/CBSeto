using System;
using System.Collections.Generic;

namespace CBSetoLib.Services
{
    public class CampBuddyCharacterService
    {
        private readonly Dictionary<string, string> _characterReactionEmojis;
        //Should change this to Dictionary<string, IEnumerable<Uri>>.
        private readonly Dictionary<string, Uri> _characterImagesUrls;
        //private readonly Dictionary<string, IEnumerable<string>> _wittyResponses;
        public CampBuddyCharacterService()
        {
            //Should probably read these from a CSV file.
            _characterReactionEmojis = new Dictionary<string, string>
            {
                {"yoichi", "🐺"},
                {"keitaro", "🐸"},
                {"hunter", "🐰"},
                {"natsumi", "🐞"},
                {"hiro", "🦝"},
                {"felix", "🦊"},
                {"seto", "🐱"},
                {"taiga", "🐯"}
            };
            _characterImagesUrls = new Dictionary<string, Uri>
            {
                {"yoichi", new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/yoichi.png")},
                {"felix", new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/felix.png")},
                {"hunter", new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/hunter.png")},
                {"keitaro", new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/keitaro.png")},
                {"seto", new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/seto.png")},
                {"taiga", new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/taiga.png")},
                {"hiro", new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/hiro-body.png")},
                {"natsumi", new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/natsumi.png")}
            };

            //_wittyResponses = new Dictionary<string, IEnumerable<string>>();
        }

        public IReadOnlyCollection<string> GetCharacterNames() => _characterReactionEmojis.Keys;

        public bool ContainsCharacterEmote(string characterName, out string emote) => 
            _characterReactionEmojis.TryGetValue(characterName.ToLower(), out emote);

        public bool ContainsCharacterImage(string characterName, out Uri imageUri) => 
            _characterImagesUrls.TryGetValue(characterName.ToLower(), out imageUri);
    }
}
