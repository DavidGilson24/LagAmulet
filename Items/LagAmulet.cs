using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
	}
}
