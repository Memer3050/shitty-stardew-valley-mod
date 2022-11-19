using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        // inits
        int item_id = 0;
        int addspeed = 0;
        int globaltimer = 0;

        bool collision = true;
        bool togglehp = false;
        bool timefuck = false;
        bool itemsword = false;
        bool money_fuck = false;

        // startup class
        public override void Entry(IModHelper helper)
		{
			helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;

            this.Monitor.Log("Why did you download this mod");
        }

        // global timer (ticks)
        private void OnUpdateTicked(object sender, EventArgs e) {

            globaltimer++;

            // godmode.

            if (togglehp == true) {
                Game1.player.stamina = Game1.player.maxStamina;
                Game1.player.health = Game1.player.maxHealth;
            }
            Game1.player.speed = Game1.player.speed + addspeed;

            // noclip code.

            if (!collision)
            {
                Game1.player.ignoreCollisions = true;
            }
            else {
                Game1.player.ignoreCollisions = false;

            }

            // time spasm

            if (timefuck == true)
            {
                Game1.timeOfDay = Game1.timeOfDay + 5;
            }

            // money spam

            if (money_fuck == true) {
                Game1.player.Money = Game1.player.Money + 10;
            }
        }

        // input class 
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
		{
            // world check

			if (!Context.IsWorldReady)
				return;

            // key = game input

            string key = e.Button.ToString();

            // add one to item_id

			if (key == "K") {
                item_id++;
                this.Monitor.Log($"{item_id} = item_id.", LogLevel.Debug);
                Game1.addHUDMessage(new HUDMessage("Item id: " + item_id, HUDMessage.error_type) { noIcon = true, number = globaltimer });
            }

            // subtract one to item_id

            if (key == "L") {
                item_id--;
                this.Monitor.Log($"{item_id} = item_id.", LogLevel.Debug);
                Game1.addHUDMessage(new HUDMessage("Item id: " + item_id, HUDMessage.error_type) { noIcon = true, number = globaltimer });
            }

            // crudely gives item_id to you

            if (key == "P") {
                if (itemsword == false) {
                    Item weapon = new MeleeWeapon(item_id);
                    Game1.player.addItemByMenuIfNecessary(weapon);
                    Game1.addHUDMessage(new HUDMessage("Weapon Given.", HUDMessage.error_type) { noIcon = true, number = globaltimer });
                }
                else if (itemsword == true)
                {
                    Item item = new StardewValley.Object(item_id, 999);
                    Game1.player.addItemByMenuIfNecessary(item);
                    Game1.addHUDMessage(new HUDMessage("Item Given.", HUDMessage.error_type) { noIcon = true, number = globaltimer });
                }
            }

            // speed editor

            if (key == "MouseX2")
            {
                addspeed++;
                Game1.player.speed = Game1.player.speed + addspeed;
                Game1.addHUDMessage(new HUDMessage("Player Speed Increase.", HUDMessage.error_type) { noIcon = true, number = globaltimer });
            }
            if (key == "MouseX1")
            {
                addspeed--;
                Game1.player.speed = Game1.player.speed + addspeed;
                Game1.addHUDMessage(new HUDMessage("Player Speed Decrease", HUDMessage.error_type) { noIcon = true, number = globaltimer });
            }

            // F% keys

            if (key == "F1")
            {
                collision = !collision;
                if (collision == true) { Game1.addHUDMessage(new HUDMessage("Collision On.", HUDMessage.error_type) { noIcon = true, number = globaltimer }); }
                if (collision == false) { Game1.addHUDMessage(new HUDMessage("Collision Off.", HUDMessage.error_type) { noIcon = true, number = globaltimer }); }
            }
            if (key == "F2")
            {
                itemsword = !itemsword;
                if (itemsword == true) { Game1.addHUDMessage(new HUDMessage("Toggled Item Spawning.", HUDMessage.error_type) { noIcon = true, number = globaltimer }); }
                if (itemsword == false) { Game1.addHUDMessage(new HUDMessage("Toggled Sword Spawning.", HUDMessage.error_type) { noIcon = true, number = globaltimer }); }
            }
            if (key == "F3") { togglehp = !togglehp; Game1.addHUDMessage(new HUDMessage("Toggled Godmode", HUDMessage.error_type) { noIcon = true, number = globaltimer }); }
            if (key == "F5") { money_fuck = !money_fuck; Game1.addHUDMessage(new HUDMessage("Money Fuckery Enabled", HUDMessage.error_type) { noIcon = true, number = globaltimer }); }
            if (key == "F8"){ timefuck = !timefuck; Game1.addHUDMessage(new HUDMessage("Time Fuck toggled", HUDMessage.error_type) { noIcon = true, number = globaltimer }); }
            if (key == "F9") { Game1.timeOfDay = 2600; Game1.addHUDMessage(new HUDMessage("Changed Time", HUDMessage.error_type) { noIcon = true, number = globaltimer }); }
        }
    }
}