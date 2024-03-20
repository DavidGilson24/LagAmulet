using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace LagAmulet.Items
{
	public class LagAmulet : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.LagAmulet.hjson file.

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.rare = ItemRarityID.Expert;
			Item.accessory = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Chain, 10);
			recipe.AddIngredient(ItemID.RainbowBrick, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            LagAmuletPlayer modPlayer = player.GetModPlayer<LagAmuletPlayer>();
            double fps = modPlayer.GetFps();

            if (fps < 21) // If FPS is under 20
            {
                player.endurance += 0.25f; // Apply 25% damage reduction.
            }
            else if (fps < 11) // If FPS is under 10
            {
                player.endurance += 0.50f; // Apply 50% damage reduction.
            }
            else if (fps < 2)
            {
                player.endurance = 0.99f;
            }
        }
	}
}

public class LagAmuletPlayer : ModPlayer
{
    private int frameCounter;
    private int lastFrameCount;
    private double lastUpdateTime;
    private double fps;

    public override void PostUpdate()
    {
        frameCounter++;

        if (Main.time - lastUpdateTime > 60)
        {
            lastUpdateTime = Main.time;
            fps = frameCounter;
            frameCounter = 0;
        }
    }

    public double GetFps()
    {
        return fps;
    }
}