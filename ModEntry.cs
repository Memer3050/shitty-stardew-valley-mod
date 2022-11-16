using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Objects;
using StardewValley.Tools;

namespace speedhackstardew
{
	public class ModEntry : Mod
	{
        int item_id = 1;
        int addspeed = 0;
        readonly bool item_safe = false;
        bool collision = true;
        Int32 globaltimer = 0;

        bool itemsword = true;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
		{
			helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        /// 

        private void OnUpdateTicked(object sender, EventArgs e) {
            globaltimer++;
            Game1.player.speed = Game1.player.speed + addspeed;
            Game1.player.stamina = 270;
            Game1.player.health = 100;
            if (!collision)
            {
                Game1.player.ignoreCollisions = true;
            }
            else {
                Game1.player.ignoreCollisions = false;

            }
        }

		private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
		{
			// ignore if player hasn't loaded a save yet
			if (!Context.IsWorldReady)
				return;

            string key = e.Button.ToString();

            // print button presses to the console window

			if (e.Button.ToString() == "K") {
                item_id++;
                if (item_safe == true)
                {
                    if (item_id > 64)
                    {
                        item_id--;
                        this.Monitor.Log($"Any ITEM_ID above 64 will cause bugs or crash the game. Press u to permanently turn off this function.", LogLevel.Warn);
                    }
                }
                this.Monitor.Log($"{item_id} = item_id.", LogLevel.Debug);
                Game1.addHUDMessage(new HUDMessage("Item id: " + item_id, HUDMessage.error_type) { noIcon = true, number = globaltimer });
            }
            if (e.Button.ToString() == "L") {
                item_id--;
                if (item_safe == true)
                {
                    if (item_id < 0)
                    {
                        item_id++;
                        this.Monitor.Log($"Any ITEM_ID below 0 will cause bugs or crash the game. Press u to permanently turn off this function.", LogLevel.Warn);
                    }
                }
                this.Monitor.Log($"{item_id} = item_id.", LogLevel.Debug);
                Game1.addHUDMessage(new HUDMessage("Item id: " + item_id, HUDMessage.error_type) { noIcon = true, number = globaltimer });
            }
            if (e.Button.ToString() == "P") {
				Item weapon = new MeleeWeapon(item_id);
                Item item = new StardewValley.Object(item_id, 999, false, -1, 0);
                if (itemsword == true) { Game1.player.addItemByMenuIfNecessary(weapon); this.Monitor.Log($"{item_id} as Weapon has been given to player.", LogLevel.Alert); Game1.addHUDMessage(new HUDMessage("Weapon Given.", HUDMessage.error_type) { noIcon = true, number = globaltimer }); return; }
                else
                {

                    Game1.player.addItemToInventory(item);
                    this.Monitor.Log($"{item_id} as Item has been given to player.", LogLevel.Alert);
                    Game1.addHUDMessage(new HUDMessage("Item Given.", HUDMessage.error_type) { noIcon = true, number = globaltimer });
                }
            }
            if (e.Button.ToString() == "MouseX2")
            {
                addspeed++;
                Game1.player.speed = Game1.player.speed + addspeed;
                this.Monitor.Log($"Player speed inc.", LogLevel.Alert);
                Game1.addHUDMessage(new HUDMessage("Player Speed Increase.", HUDMessage.error_type) { noIcon = true, number = globaltimer });
            }
            if (e.Button.ToString() == "MouseX1")
            {
                addspeed--;
                Game1.player.speed = Game1.player.speed + addspeed;
                this.Monitor.Log($"Player speed dec.", LogLevel.Alert);
                Game1.addHUDMessage(new HUDMessage("Player Speed Decrease", HUDMessage.error_type) { noIcon = true, number = globaltimer });
            }


            if (e.Button.ToString() == "NumPad8")
            {
                Game1.player.position.Y = Game1.player.position.Y - 50;
            }
            if (e.Button.ToString() == "NumPad2")
            {
                Game1.player.position.Y = Game1.player.position.Y + 50;
            }
            if (e.Button.ToString() == "NumPad4")
            {
                Game1.player.position.X = Game1.player.position.X - 50;
            }
            if (e.Button.ToString() == "NumPad6")
            {
                Game1.player.position.X = Game1.player.position.X + 50;
            }

            if (e.Button.ToString() == "F1")
            {
                collision = !collision;
                if (collision == true)
                {
                    Game1.addHUDMessage(new HUDMessage("Collision On.", HUDMessage.error_type) { noIcon = true, number = globaltimer });
                }
                if (collision == false)
                {
                    Game1.addHUDMessage(new HUDMessage("Collision Off.", HUDMessage.error_type) { noIcon = true, number = globaltimer });
                }
                
            }
            if (e.Button.ToString() == "F2")
            {
                itemsword = !itemsword;
                Game1.addHUDMessage(new HUDMessage("Toggled Item / Sword Spawning.", HUDMessage.error_type) { noIcon = true, number = globaltimer });

            }



        }

	}
}
