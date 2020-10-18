using System;
using System.Collections.Generic;
using System.Linq;
using CBSetoLib.Models;

namespace CBSetoLib.Services
{
    public class CampBuddyCharacterService
    {
        private readonly Dictionary<string, string> _characterReactionEmojis;
        private readonly Dictionary<string, int> _characterIds;
        private readonly List<Character> _characters;
        public CampBuddyCharacterService()
        { 
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
            _characterIds = new Dictionary<string, int>
            {
                {"seto", 724},
                {"shintaro", 724},
                {"felix", 926},
                {"eduard", 530},
                {"lee", 842},
                {"keitaro", 485},
                {"hiro", 922},
                {"hunter", 320},
                {"yoichi", 121},
                {"natsumi", 620},
                {"kieran", 800},
                {"naoto", 126},
                {"yoshi", 307},
                {"yoshinori", 307},
                {"goro", 703},
                {"taiga", 713}
            };
            _characters = new List<Character>(GetCharacters());
        }

        public IReadOnlyCollection<string> GetCharacterNames() => _characterReactionEmojis.Keys;

        public bool ContainsCharacterEmote(string characterName, out string emote) => 
            _characterReactionEmojis.TryGetValue(characterName.ToLower(), out emote);

        public bool TryGetCharacter(string characterName, out Character character)
        {
            if (_characterIds.TryGetValue(characterName.ToLower(), out int characterId))
            {
                character = _characters.FirstOrDefault(c => c.Id == characterId);
                return true;
            }
            character = null;
            return false;
        }

        public IEnumerable<Character> GetCharacters()
        {
            yield return new Character
            {
                Id = 724,
                ProfileUri = new Uri("https://www.blitsgames.com/project/seto-aihara/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/seto.png"),
                Info = new Info
                {
                    Name = "Seto Aihara",
                    Height = 165,
                    Weight = 52
                },
                Special = new Special
                {
                    Caption = "Online Sex",
                    Description = "Seto has creative ways to use his gadgets..."
                },
                Position = new Position
                {
                    Top = 0.60, Bottom = 0.40
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 5.20, ErectWidth = 1.37, ErectGirth = 4.30,
                    FlaccidLength = 3.75, FlaccidWidth = 1.11, FlaccidGirth = 3.50
                }
            };
            yield return new Character
            {
                Id = 926,
                ProfileUri = new Uri("https://www.blitsgames.com/project/felix-clermont/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/felix.png"),
                Info = new Info
                {
                    Name = "Felix Clermont",
                    Height = 157,
                    Weight = 49
                },
                Special = new Special
                {
                    Caption = "Socks",
                    Description = "Felix keeps his legs flawless by wearing long socks all the time."
                },
                Position = new Position
                {
                    Top = 0,
                    Bottom = 1
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 5.25,
                    ErectWidth = 1.24,
                    ErectGirth = 3.90,
                    FlaccidLength = 2.75,
                    FlaccidWidth = 1.05,
                    FlaccidGirth = 3.30
                }
            };
            yield return new Character
            {
                Id = 530,
                ProfileUri = new Uri("https://www.blitsgames.com/project/eduard-fitzpatrick/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/eduard.png"),
                Info = new Info
                {
                    Name = "Eduard Fitzpatrick",
                    Height = 165,
                    Weight = 50
                },
                Special = new Special
                {
                    Caption = "Crossdress",
                    Description = "Eduard would definitely say yes to wearing a dress."
                },
                Position = new Position
                {
                    Top = 0,
                    Bottom = 1
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 5,
                    ErectWidth = 1.27,
                    ErectGirth = 4,
                    FlaccidLength = 3.2,
                    FlaccidWidth = 1.19,
                    FlaccidGirth = 3.75
                }
            };
            yield return new Character
            {
                Id = 842,
                ProfileUri = new Uri("https://www.blitsgames.com/project/lee-kurosawa/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/lee.png"),
                Info = new Info
                {
                    Name = "Lee Kurosawa",
                    Height = 164,
                    Weight = 49
                },
                Special = new Special
                {
                    Caption = "Role Play",
                    Description = "Lee had something different in mind with the word \"roleplay\""
                },
                Position = new Position
                {
                    Top = 0.1,
                    Bottom = 0.9
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 4.5,
                    ErectWidth = 1.43,
                    ErectGirth = 4.5,
                    FlaccidLength = 4,
                    FlaccidWidth = 1.34,
                    FlaccidGirth = 4.2
                }
            };
            yield return new Character
            {
                Id = 485,
                ProfileUri = new Uri("https://www.blitsgames.com/project/keitaro-nagame/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/keitaro.png"),
                Info = new Info
                {
                    Name = "Keitaro Nagame",
                    Height = 168,
                    Weight = 55
                },
                Special = new Special
                {
                    Caption = "Blank Slate",
                    Description = "Keitaro's special trait is his open-mindedness with sexual activity. To him, pleasing his partner is the top priority and he is always ready to explore or even tone things down, making him easily compatible with his partner"
                },
                Stats = new Stats
                {
                    Stamina = 1, Power = 2, Skill = 4, Tolerance = 3, Libido = 5
                },
                Position = new Position
                {
                    Top = 0.5,
                    Bottom = 0.5
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 5.55,
                    ErectWidth = 1.6,
                    ErectGirth = 5.02,
                    FlaccidLength = 3,
                    FlaccidWidth = 1.4,
                    FlaccidGirth = 4.4
                }, 
                RearProfile = new RearProfile
                {
                    Waist = 28, Hip = 37
                }
            };
            yield return new Character
            {
                Id = 922,
                ProfileUri = new Uri("https://www.blitsgames.com/project/hiro-akiba/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/hiro-body.png"),
                Info = new Info
                {
                    Name = "Hiro Akiba",
                    Height = 166,
                    Weight = 55
                },
                Special = new Special
                {
                    Caption = "Food Play",
                    Description = "Hiro's love for food seems to have mixed in with his naughty side - whether serving himself as the main course or slurping up the extra cream filling, Hiro will guarantee you a five-star experience."
                },
                Stats = new Stats
                {
                    Stamina = 2, Power = 2, Skill = 5, Tolerance = 4, Libido = 3
                },
                Position = new Position
                {
                    Top = 0.3,
                    Bottom = 0.7
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 5.25,
                    ErectWidth = 1.5,
                    ErectGirth = 4.17,
                    FlaccidLength = 3.5,
                    FlaccidWidth = 1.3,
                    FlaccidGirth = 4.08
                },
                RearProfile = new RearProfile
                {
                    Waist = 28, Hip = 36
                }
            };
            yield return new Character
            {
                Id = 320,
                ProfileUri = new Uri("https://www.blitsgames.com/project/hunter-springfield/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/hunter.png"),
                Info = new Info
                {
                    Name = "Hunter Springfield",
                    Height = 160, Weight = 51
                },
                Special = new Special
                {
                    Caption = "Tickling",
                    Description = "Hunter's sensitive spots don't just stop at his privates - thanks to his smooth skin, he's quite ticklish all over, and even enjoys the sensation! A light feather on the foot will have him asking for more in no time~"
                },
                Stats = new Stats
                {
                    Stamina = 2, Power = 1, Skill = 3, Tolerance = 5, Libido = 3
                },
                Position = new Position
                {
                    Top = 0.1, Bottom = 0.9
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 5, ErectWidth = 1.25, ErectGirth = 3.92,
                    FlaccidLength = 3, FlaccidWidth = 1.2, FlaccidGirth = 3.8
                },
                RearProfile = new RearProfile
                {
                    Hip = 38, Waist = 27
                }
            };
            yield return new Character
            {
                Id = 121,
                ProfileUri = new Uri("https://www.blitsgames.com/project/yoichi-yukimura/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/yoichi.png"),
                Info = new Info
                {
                    Name = "Yoichi Yukimura",
                    Height = 170,
                    Weight = 69
                },
                Special = new Special
                {
                    Caption = "Bondage",
                    Description = "With Yoichi being naturally dominant, it's impossible to make him admit his interest in being tied up and dominated. This most likely relates to his animalistic tendencies during sex."
                },
                Stats = new Stats
                {
                    Libido = 4, Power = 5, Skill = 1, Tolerance = 2, Stamina = 3
                },
                Position = new Position
                {
                    Top = 0.7,
                    Bottom = 0.3
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 6.5,
                    ErectWidth = 2.15,
                    ErectGirth = 6.75,
                    FlaccidLength = 4.3,
                    FlaccidWidth = 2,
                    FlaccidGirth = 6.3
                },
                RearProfile = new RearProfile
                {
                    Hip = 39, Waist = 33
                }
            };
            yield return new Character
            {
                Id = 620,
                ProfileUri = new Uri("https://www.blitsgames.com/project/natsumi-hamasaki/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/natsumi.png"),
                Info = new Info
                {
                    Name = "Natsumi Hamasaki",
                    Height = 179, Weight = 62
                },
                Special = new Special
                {
                    Caption = "Underwear",
                    Description = "Having a slender and well-built body makes Natsumi a good candidate for an underwear model. It seems that trying on different kinds of them gives himma huge confidence boost to show off what he's packing!"
                },
                Stats = new Stats
                {
                    Libido = 4, Power = 3, Skill = 2, Tolerance = 1, Stamina = 5
                },
                Position = new Position
                {
                    Top = 0.7, Bottom = 0.3
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 7.75, ErectWidth = 1.75, ErectGirth = 5.50,
                    FlaccidLength = 5.75, FlaccidWidth = 1.75, FlaccidGirth = 5.5
                },
                RearProfile = new RearProfile
                {
                    Hip = 35, Waist = 30
                }
            };
            yield return new Character
            {
                Id = 800,
                ProfileUri = new Uri("https://www.blitsgames.com/project/kieran-moreno/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/11/kieran.png"),
                Info = new Info
                {
                    Name = "Kieran Moreno",
                    Height = 169,
                    Weight = 54
                },
                Special = new Special
                {
                    Caption = "Massage",
                    Description = "Kieren loves giving massages as opposed to recieving them."
                },
                Position = new Position
                {
                    Top = 0.4,
                    Bottom = 0.6
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 6,
                    ErectWidth = 1.75,
                    ErectGirth = 5.5,
                    FlaccidLength = 4.5,
                    FlaccidWidth = 1.62,
                    FlaccidGirth = 5.1
                }
            };
            yield return new Character
            {
                Id = 126,
                ProfileUri = new Uri("https://www.blitsgames.com/project/naoto-hamasaki/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/naoto.png"),
                Info = new Info
                {
                    Name = "Naoto Hamasaki",
                    Height = 185,
                    Weight = 84
                },
                Special = new Special
                {
                    Caption = "Modeling",
                    Description = "Naoto enjoys the confidence his modeling job gives more than people think..."
                },
                Position = new Position
                {
                    Top = 0.7,
                    Bottom = 0.3
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 8.55,
                    ErectWidth = 1.91,
                    ErectGirth = 6,
                    FlaccidLength = 7.3,
                    FlaccidWidth = 1.83,
                    FlaccidGirth = 5.75
                }
            };
            yield return new Character
            {
                Id = 307,
                ProfileUri = new Uri("https://www.blitsgames.com/project/yoshinori-nagira/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/yoshi.png"),
                Info = new Info
                {
                    Name = "Yoshinori Nagira",
                    Height = 188,
                    Weight = 85
                },
                Special = new Special
                {
                    Caption = "Boots",
                    Description = "Being active at camp Yoshinori always wears his trusty boots and he wouldn't mind keeping them on when having fun sometimes."
                },
                Position = new Position
                {
                    Top = 0.5,
                    Bottom = 0.5
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 7.3,
                    ErectWidth = 2.07,
                    ErectGirth = 6.5,
                    FlaccidLength = 6.8,
                    FlaccidWidth = 2.07,
                    FlaccidGirth = 6.5
                }
            };
            yield return new Character
            {
                Id = 703,
                ProfileUri = new Uri("https://www.blitsgames.com/project/goro-nomoru/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/goro.png"),
                Info = new Info
                {
                    Name = "Goro Nomoru",
                    Height = 196,
                    Weight = 115
                },
                Special = new Special
                {
                    Caption = "leather",
                    Description = "Goro likes the touch of leather tightly wrapping around his bulky muscles. The scent and sound it makes also stimulates him."
                },
                Position = new Position
                {
                    Top = 1.0,
                    Bottom = 0.0
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 10.75,
                    ErectWidth = 2.07,
                    ErectGirth = 7.5,
                    FlaccidLength = 10,
                    FlaccidWidth = 2.23,
                    FlaccidGirth = 7
                }
            };
            yield return new Character
            {
                Id = 609,
                ProfileUri = new Uri("https://www.blitsgames.com/project/aiden-flynn/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/aiden.png"),
                Info = new Info
                {
                    Name = "Aiden Flynn",
                    Height = 183,
                    Weight = 88
                },
                Special = new Special
                {
                    Caption = "Exhibitionism",
                    Description = "Aiden likes to hang ot loose and show off the body he works so hard to build. He defintely wouldn't mind putting on a show completely naked."
                },
                Position = new Position
                {
                    Top = 0.3,
                    Bottom = 0.7
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 8,
                    ErectWidth = 2.01,
                    ErectGirth = 6.3,
                    FlaccidLength = 7,
                    FlaccidWidth = 2.01,
                    FlaccidGirth = 6.3
                }
            };
            yield return new Character
            {
                Id = 713,
                ProfileUri = new Uri("https://www.blitsgames.com/project/taiga-akatora/"),
                ImageUri = new Uri("https://www.blitsgames.com/wp-content/uploads/2019/01/taiga.png"),
                Info = new Info
                {
                    Name = "Taiga Akatora",
                    Height = 168,
                    Weight = 56
                },
                Stats = new Stats
                {
                    Libido = 3,Power = 4, Skill = 4, Stamina = 4, Tolerance = 3
                },
                Special = new Special
                {
                    Caption = "Massage",
                    Description = "Taiga's all about massages, specifically when he's the one getting the rubdown! To him, a massage by that special someone is a treat, as he loves to be served and spoiled. He also feels like it's an opportunity for his partner to worship his hot bod!"
                },
                Position = new Position
                {
                    Top = 0.5,
                    Bottom = 0.5
                },
                FrontProfile = new FrontProfile
                {
                    ErectLength = 5.55,
                    ErectWidth = 1.75,
                    ErectGirth = 5.50,
                    FlaccidLength = 4.5,
                    FlaccidWidth = 1.6,
                    FlaccidGirth = 5.02
                }
            };
        }
    }
}
