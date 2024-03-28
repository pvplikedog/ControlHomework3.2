// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

using MenuClassLibrary;
using PlayerJSONClassLibrary;

namespace ControlHomework32
{
    /// <summary>
    /// Class that holds all menu logic for exactly this programm.
    /// </summary>
    public class MenuWorker
    {
        // MenuSheet properties. I feel like it should be public, so you can change some menuSheet outside this class.
        public MenuSheet UserAgreement { get; set; }
        public MenuSheet UserAgreementFailed { get; set; }
        public MenuSheet FileReaden { get; set; }
        public MenuSheet FileSort { get; set; }
        public MenuSheet FileEdit { get; set; }
        public MenuSheet FileSave { get; set; }
        public MenuSheet FileSuccessfullySaved { get; set; }
        public MenuSheet PlayerEdit { get; set; }
        public MenuSheet UsernameEdit { get; set; }
        public MenuSheet LevelEdit { get; set; }
        public MenuSheet GuildEdit { get; set; }
        public MenuSheet AchievementEdit { get; set; }
        public MenuSheet AchievementChoose { get; set; }
        public MenuSheet AchievementNameEdit { get; set; }
        public MenuSheet AchievementDescriptionEdit { get; set; }
        public MenuSheet AchievementLevelEdit { get; set; }

        public PlayerListWorker PlayerList { get; set; }

        private Player _currentPlayer;
        private Achievement _currentAchievement;

        /// <summary>
        /// Continue on agreement sheet.
        /// </summary>
        private void ContinueOnAgreementSheet(object? sender, EventArgs e)
        {
            if (UserAgreement.GetMenuOptions()[0])
            {
                PlayerList = new PlayerListWorker(true);
                OpenFileReadenSheet(sender, e);
            }
            else
            {
                UserAgreementFailed.HandleMenu();
            }
        }

        /// <summary>
        /// Start the menu.
        /// </summary>
        public void StartMenu()
        {
            UserAgreement.HandleMenu();
        }

        /// <summary>
        /// Exit the application.
        /// </summary>
        private void Exit(object? sender, EventArgs e)
        {
            ConsoleHandler.Exit();
        }

        /// <summary>
        /// Show file.
        /// </summary>
        private void ViewFile(object? sender, EventArgs e)
        {
            PlayerListWritter.ShowPlayers(PlayerList.Players);
            OpenFileReadenSheet(sender, e);
        }

        /// <summary>
        /// Open menu when file is read.
        /// </summary>
        private void OpenFileReadenSheet(object? sender, EventArgs e)
        {
            FileReaden.HandleMenu();
        }

        /// <summary>
        /// Open menu to choose what player to edit.
        /// </summary>
        private void FileEditSheet(object? sender, EventArgs e)
        {
            FileEdit = new MenuSheet(new[] { "Choose what player you want to edit:" }, GetPlayersIdAsButtons());
            FileEdit.HandleMenu();
        }

        /// <summary>
        /// Save file in path.
        /// </summary>
        /// <param name="path">Path to save file in.</param>
        private void SaveFile(string path)
        {
            try
            {
                JsonWorker.WriteJson(PlayerList.Players, path);
                FileSuccessfullySaved.HandleMenu();
            }
            catch (IOException)
            {
                ConsoleHandler.PrintMessage("Something went wrong when trying to save the file. Please try again.");
                FileSave.HandleMenu();
            }
            catch
            {
                ConsoleHandler.PrintMessage("Something went wrong. Please try again.");
                FileSave.HandleMenu();
            }
        }

        /// <summary>
        /// Sort the file by choosen options.
        /// </summary>
        private void ContinueOnSortSheet(object? sender, EventArgs e)
        {
            if (FileSort.GetMenuOptions()[0])
                PlayerList.SortPlayerList(PlayerListWorker.SortTarget.player_id, FileSort.GetMenuOptions()[6]);
            else if (FileSort.GetMenuOptions()[1])
                PlayerList.SortPlayerList(PlayerListWorker.SortTarget.username, FileSort.GetMenuOptions()[6]);
            else if (FileSort.GetMenuOptions()[2])
                PlayerList.SortPlayerList(PlayerListWorker.SortTarget.level, FileSort.GetMenuOptions()[6]);
            else if (FileSort.GetMenuOptions()[3])
                PlayerList.SortPlayerList(PlayerListWorker.SortTarget.game_score, FileSort.GetMenuOptions()[6]);
            else if (FileSort.GetMenuOptions()[4])
                PlayerList.SortPlayerList(PlayerListWorker.SortTarget.guild, FileSort.GetMenuOptions()[6]);
            else if (FileSort.GetMenuOptions()[5])
                PlayerList.SortPlayerList(PlayerListWorker.SortTarget.achievements_amount, FileSort.GetMenuOptions()[6]);
            ConsoleHandler.PrintMessage("File successfully sorted!", 1000);
            FileReaden.HandleMenu();
        }

        /// <summary>
        /// Choose what player to edit by pressed button.
        /// </summary>
        private void PlayerToEditChoose(object? sender, EventArgs e)
        {
            _currentPlayer = PlayerList.GetPlayerFromListById(((Button)sender!).Text);
            PlayerEditLoad(sender, e);
        }

        /// <summary>
        /// Open menu with player edit.
        /// </summary>
        private void PlayerEditLoad(object? sender, EventArgs e)
        {
            PlayerEdit = new MenuSheet(new[] { $"Currently editing player: {_currentPlayer.Id}",
                "Choose what field you want to edit:" }, GetPlayerFieldsAsButtons(_currentPlayer));
            PlayerEdit.HandleMenu();
        }

        /// <summary>
        /// Add random generated player.
        /// </summary>
        private void AddPlayer(object? sender, EventArgs e)
        {
            _currentPlayer = RandomGenerator.GenerateRandomPlayer();
            PlayerList.AddPlayerToPlayerList(_currentPlayer);
            PlayerEditLoad(sender, e);
        }

        /// <summary>
        /// Add random generated achievement.
        /// </summary>
        private void AddAchievement(object? sender, EventArgs e)
        {
            _currentAchievement = RandomGenerator.GenerateRandomAchievement();
            PlayerList.AddAchievementToPlayer(_currentAchievement, _currentPlayer);
            AchievementEditLoad(sender, e);
        }

        /// <summary>
        /// Delete choosen achievement.
        /// </summary>
        private void DeleteAchievement(object? sender, EventArgs e)
        {
            PlayerList.RemoveAchievementFromPlayer(_currentAchievement, _currentPlayer);
            EditPlayerAchievements(sender, e);
        }

        /// <summary>
        /// Delete choosen player.
        /// </summary>
        private void DeletePlayer(object? sender, EventArgs e)
        {
            PlayerList.RemovePlayerFromList(_currentPlayer);
            FileEditSheet(sender, e);
        }

        /// <summary>
        /// Open menu to choose what achievement to edit.
        /// </summary>
        private void EditPlayerAchievements(object? sender, EventArgs e)
        {
            AchievementChoose = new MenuSheet(new[] { "Choose what achievement you want to edit:" }, GetAchievementsIdAsButtons());
            AchievementChoose.HandleMenu();
        }

        /// <summary>
        /// Generates clickable buttons from achievements id.
        /// </summary>
        /// <returns>Clickable buttons with id as text.</returns>
        private Button[] GetAchievementsIdAsButtons()
        {
            Button[] buttons = new Button[_currentPlayer.Achievements.Count + 2];
            for (int curAchievement = 0; curAchievement < _currentPlayer.Achievements.Count; curAchievement++)
            {
                buttons[curAchievement] = new ButtonClickable(_currentPlayer.Achievements[curAchievement].Id, AchievementToEditChoose);
            }

            buttons[^2] = new ButtonClickable("add achievement", AddAchievement);
            buttons[^1] = new ButtonClickable("return", PlayerEditLoad);

            return buttons;
        }

        /// <summary>
        /// Choose what achievement to edit by pressed button.
        /// </summary>
        private void AchievementToEditChoose(object? sender, EventArgs e)
        {
            _currentAchievement = PlayerList.GetAchievementFromPlayerById(((Button)sender!).Text, _currentPlayer);
            AchievementEditLoad(sender, e);
        }

        /// <summary>
        /// Open menu with achievement edit.
        /// </summary>
        private void AchievementEditLoad(object? sender, EventArgs e)
        {
            AchievementEdit = new MenuSheet(new[] { $"Currently editing achievement: {_currentAchievement.Id}",
                "Choose what field you want to edit:" }, GetAchievementsFieldsAsButtons(_currentAchievement));
            AchievementEdit.HandleMenu();
        }

        /// <summary>
        /// Generates clickable buttons from achievement fields.
        /// </summary>
        /// <param name="achievement">Achievement what from generate buttons.</param>
        /// <returns>Clickable buttons with button's fields as text.</returns>
        private Button[] GetAchievementsFieldsAsButtons(Achievement achievement)
        {
            Button[] buttons = new Button[] {
                new ButtonClickable($"name: {achievement.Name}", EditAchievementNameSheet),
                new ButtonClickable($"description: {achievement.Description}", EditAchievementDescriptionSheet),
                new ButtonClickable($"points: {achievement.Points}", EditAchievementPointsSheet),
                new ButtonClickable("delete this achievement", DeleteAchievement),
                new ButtonClickable("return", EditPlayerAchievements)
            };
            return buttons;
        }

        /// <summary>
        /// Open achievement points edit menu.
        /// </summary>
        private void EditAchievementPointsSheet(object? sender, EventArgs e)
        {
            // Generates Menu Sheet.
            AchievementLevelEdit = new MenuSheet(new[] { $"Currently editing points of achievement: {_currentAchievement.Id}",
            $"Currnet points: {_currentAchievement.Points}. How you want to edit it?"},
            new Button[] {new ButtonClickable("add 1", (sender, e) =>
            {
                _currentAchievement.Points++;
                EditAchievementPointsSheet(sender, e);
            }),
            new ButtonClickable("add 10", (sender, e) =>
            {
                _currentAchievement.Points += 10;
                EditAchievementPointsSheet(sender, e);
            }),
            new ButtonClickable("remove 1", (sender, e) =>
            {
                _currentAchievement.Points--;
                EditAchievementPointsSheet(sender, e);
            }),
            new ButtonClickable("input points", (sender, e) => {
                _currentAchievement.Points = ConsoleHandler.GetIntFromUser("Input points: ");
                EditAchievementPointsSheet(sender, e);
            }), new ButtonClickable("return", PlayerEditLoad)}
            );
            AchievementLevelEdit.HandleMenu();
        }

        /// <summary>
        /// Open achievement description edit menu.
        /// </summary>
        private void EditAchievementDescriptionSheet(object? sender, EventArgs e)
        {
            // Generates Menu Sheet.
            AchievementDescriptionEdit = new MenuSheet(new[] { $"Currently editing description of achievement: {_currentAchievement.Id}",
            $"Currnet desctiption: {_currentAchievement.Description} How you want to edit it?"},
            new Button[] {new ButtonClickable("input new description", (sender, e) => {
                _currentAchievement.Description = ConsoleHandler.GetStringFromUser("Input new description: ");
                EditAchievementDescriptionSheet(sender, e);
            }),
            new ButtonClickable("generate new description", (sender, e) =>
            {
                _currentAchievement.Description = RandomGenerator.GenerateRandomDescription();
                EditAchievementDescriptionSheet(sender, e);
            }),
            new ButtonClickable("return", AchievementEditLoad)
            });
            AchievementDescriptionEdit.HandleMenu();
        }

        /// <summary>
        /// Open achievement name edit menu.
        /// </summary>
        private void EditAchievementNameSheet(object? sender, EventArgs e)
        {
            // Generates Menu Sheet.
            AchievementNameEdit = new MenuSheet(new[] { $"Currently editing name of achievement: {_currentAchievement.Id}",
            $"Currnet name: {_currentAchievement.Name}. How you want to edit it?"},
            new Button[] {new ButtonClickable("input new name", (sender, e) => {
                _currentAchievement.Name = ConsoleHandler.GetStringFromUser("Input new name: ");
                EditAchievementNameSheet(sender, e);
            }),
            new ButtonClickable("generate new name", (sender, e) =>
            {
                _currentAchievement.Name = RandomGenerator.GenerateRandomName();
                EditAchievementNameSheet(sender, e);
            }),
            new ButtonClickable("return", AchievementEditLoad)
            });
            AchievementNameEdit.HandleMenu();
        }

        /// <summary>
        /// Open username edit menu.
        /// </summary>
        private void EditUsernameSheet(object? sender, EventArgs e)
        {
            // Generates Menu Sheet.
            UsernameEdit = new MenuSheet(new[] { $"Currently editing username of player: {_currentPlayer.Id}",
            $"Currnet username: {_currentPlayer.Username}. How you want to edit it?"},
            new Button[] {new ButtonClickable("input new username", (sender, e) => {
                _currentPlayer.Username = ConsoleHandler.GetStringFromUser("Input new username: ");
                EditUsernameSheet(sender, e);
            }),
            new ButtonClickable("generate new username", (sender, e) =>
            {
                _currentPlayer.Username = RandomGenerator.GenerateRandomUsername();
                EditUsernameSheet(sender, e);
            }),
            new ButtonClickable("return", PlayerEditLoad)
            });
            UsernameEdit.HandleMenu();
        }

        /// <summary>
        /// Open guild edit menu.
        /// </summary>
        private void EditGuildSheet(object? sedner, EventArgs e)
        {
            // Generates Menu Sheet.
            GuildEdit = new MenuSheet(new[] { $"Currently editing guild of player: {_currentPlayer.Id}",
            $"Currnet username: {_currentPlayer.Guild}. How you want to edit it?"},
            new Button[] {new ButtonClickable("input new guild", (sender, e) => {
                _currentPlayer.Guild = ConsoleHandler.GetStringFromUser("Input guild name: ");
                EditGuildSheet(sender, e);
            }),
            new ButtonClickable("generate guild name", (sender, e) =>
            {
                _currentPlayer.Guild = RandomGenerator.GenerateRandomName();
                EditGuildSheet(sender, e);
            }),
            new ButtonClickable("return", PlayerEditLoad)
            });
            GuildEdit.HandleMenu();
        }

        /// <summary>
        /// Open level edit menu.
        /// </summary>
        private void EditLevelSheet(object? sender, EventArgs e)
        {
            // Generates Menu Sheet.
            LevelEdit = new MenuSheet(new[] { $"Currently editing level of player: {_currentPlayer.Id}",
            $"Currnet level: {_currentPlayer.Level}. How you want to edit it?"},
            new Button[] {new ButtonClickable("add 1", (sender, e) =>
            {
                _currentPlayer.Level++;
                EditLevelSheet(sender, e);
            }),
            new ButtonClickable("add 10", (sender, e) =>
            {
                _currentPlayer.Level += 10;
                EditLevelSheet(sender, e);
            }),
            new ButtonClickable("remove 1", (sender, e) =>
            {
                _currentPlayer.Level--;
                EditLevelSheet(sender, e);
            }),
            new ButtonClickable("input level", (sender, e) => {
                _currentPlayer.Level = ConsoleHandler.GetIntFromUser("Input level: ");
                EditLevelSheet(sender, e);
            }), new ButtonClickable("return", PlayerEditLoad)}
            );
            LevelEdit.HandleMenu();
        }

        /// <summary>
        /// Generates clickable buttons from player fields.
        /// </summary>
        /// <param name="player">Player from what generate buttons.</param>
        /// <returns>Clickable buttons with player's fields as text.</returns>
        private Button[] GetPlayerFieldsAsButtons(Player player)
        {
            Button[] buttons = new Button[] {
                new ButtonClickable($"username: {player.Username}", EditUsernameSheet),
                new ButtonClickable($"level: {player.Level}", EditLevelSheet),
                new ButtonClickable($"guild: {player.Guild}", EditGuildSheet),
                new ButtonClickable($"achievements: {player.Achievements.Count}", EditPlayerAchievements),
                new ButtonClickable("delete this player", DeletePlayer),
                new ButtonClickable("return", FileEditSheet)
            };
            return buttons;
        }

        /// <summary>
        /// Generates clickable buttons from players ids.
        /// </summary>
        /// <returns>Clickable buttons with players ids as text.</returns>
        private Button[] GetPlayersIdAsButtons()
        {
            Button[] buttons = new Button[PlayerList.Players.Count + 2];
            for (int curPlayer = 0; curPlayer < PlayerList.Players.Count; curPlayer++)
            {
                buttons[curPlayer] = new ButtonClickable(PlayerList.Players[curPlayer].Id, PlayerToEditChoose);
            }

            buttons[^2] = new ButtonClickable("add player", AddPlayer);
            buttons[^1] = new ButtonClickable("return", OpenFileReadenSheet);

            return buttons;
        }

        public MenuWorker()
        {
            // Null protection(to remove warnings).
            _currentAchievement = new Achievement();
            _currentPlayer = new Player();
            FileEdit = PlayerEdit = UsernameEdit = LevelEdit = GuildEdit = AchievementEdit =
                AchievementChoose = AchievementNameEdit = AchievementDescriptionEdit =
                AchievementLevelEdit = new MenuSheet();
            PlayerList = new PlayerListWorker();

            /// The program is designed in such a way that null in menuSheet cannot appear out of nowhere,
            /// so I put ! next to MenuSheet.HandleMenu().
            UserAgreement = new MenuSheet(new[] { "This program was developed as a control homework 3.2, option 13" +
                " by Anohin Anton BPI2311. \nIt allows you to read a json file with players and change it." +
                " \nMost of the work was dedicated to changing the file, so make sure you check it out! \nAlso," +
                " this program automatically saves the file if less than 15 seconds have passed since the last" +
                " change. \nTemp file is stored next to the exe file. \nRead the readme file for more " +
                "information and clarification of some points." },
                new Button[] { new ButtonCheckable("I have read and agree to the terms of the user agreement"),
                    new ButtonClickable("continue", ContinueOnAgreementSheet)});

            UserAgreementFailed = new MenuSheet(new[] { "You have not agreed to the user agreement." },
                new Button[] { new ButtonClickable("return", (sender, e) => { UserAgreement.HandleMenu(); }),
                new ButtonClickable("exit", Exit)
                });

            FileReaden = new MenuSheet(new[] { "The file was read successfully. Choose what you want to do:" },
                new Button[] { new ButtonClickable("view file", ViewFile),
                new ButtonClickable("sort contents", (sender, e) => {FileSort!.HandleMenu(); }),
                new ButtonClickable("edit contents", FileEditSheet),
                new ButtonClickable("save", (sender, e) => {FileSave!.HandleMenu(); }),
                new ButtonClickable("read another file", ContinueOnAgreementSheet),
                new ButtonClickable("exit", Exit)});

            ButtonChoosable[] sortButtonsChoosable = new ButtonChoosable[] {new ButtonChoosable("player_id"),
            new ButtonChoosable("username"), new ButtonChoosable("level"), new ButtonChoosable("game_score"),
            new ButtonChoosable("guild"), new ButtonChoosable("achievments")};
            ButtonChoosable.LinkAllButtons(sortButtonsChoosable);
            FileSort = new MenuSheet(new[] { "Choose what field you want to sort by:" },
                new Button[] { sortButtonsChoosable[0], sortButtonsChoosable[1], sortButtonsChoosable[2],
                sortButtonsChoosable[3], sortButtonsChoosable[4], sortButtonsChoosable[5], new ButtonCheckable("sort in reverse order"),
                new ButtonClickable("continue", ContinueOnSortSheet),
                new ButtonClickable("return", OpenFileReadenSheet)});

            FileSave = new MenuSheet(new[] { "You want to save file in:" }, new Button[] {
                new ButtonClickable("current file", (sender, e) => {SaveFile(PlayerList!.OriginalFilePath);}),
                new ButtonClickable("new path", (sender, e) => {SaveFile(ConsoleHandler.GetFilePathFromUser()); }),
                new ButtonClickable("in the same directory as the current file", (sender, e) =>
                { SaveFile($"{Path.GetDirectoryName(PlayerList!.OriginalFilePath)}" + Path.DirectorySeparatorChar +
                        $"{ConsoleHandler.GetStringFromUser("Input the file name: ")}.json"); }),
                new ButtonClickable("return", OpenFileReadenSheet)
            });

            FileSuccessfullySaved = new MenuSheet(new[] { "File successfully saved! What you want to do next?" },
                new Button[] { new ButtonClickable("return to file editing", OpenFileReadenSheet) ,
                new ButtonClickable("open another file", ContinueOnAgreementSheet),
                new ButtonClickable("exit", Exit)});

        }
    }
}
