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

			player.endurance -= player.GetModPlayer<LagAmuletPlayer>().enduranceBonus;
			player.GetModPlayer<LagAmuletPlayer>().enduranceBonus = 0f;

			if (fps < 2)
			{
				player.GetModPlayer<LagAmuletPlayer>().enduranceBonus = 0.99f;
			}
			else if (fps < 11) 
			{
				player.GetModPlayer<LagAmuletPlayer>().enduranceBonus = 0.50f;
			}
			else if (fps < 21)
			{
				player.GetModPlayer<LagAmuletPlayer>().enduranceBonus = 0.25f;
			}

			// Apply the calculated bonus
			player.endurance += player.GetModPlayer<LagAmuletPlayer>().enduranceBonus;
		}

	}
}

public class LagAmuletPlayer : ModPlayer
{
    private double lastUpdateTime = -1;
    private double fps = 0;

    public override void PostUpdate()
    {
        double currentTime = (double)Main.GameUpdateCount / 60.0; // there are 60 ticks per second
        if (lastUpdateTime >= 0)
        {
            double deltaTime = currentTime - lastUpdateTime;
            fps = 1 / deltaTime; // Calculate FPS
        }
        lastUpdateTime = currentTime;
    }

    public double GetFps()
    {
        return fps;
    }

	public float enduranceBonus = 0f;
    public override void ResetEffects()
    {
        enduranceBonus = 0f;
    }
}
