using System.Numerics;
using Raylib_cs;

public class UIPower
{
    static readonly IAssetsManager assets = Services.Get<IAssetsManager>();

    public static void DrawPowerDisplayed(Character characterOrigin)
    {
        PowerSystem powerSystem = characterOrigin.GetPowerSystem();

        // First display the square around the Power
        int i_SizeSquare = 100;
        int i_PY = (int)(Raylib.GetScreenHeight() * 0.8f);;
        int i_PXBonus, i_PXMalus;
        Rectangle recPower;
        
        i_PXBonus = Raylib.GetScreenWidth() / 2 - 150 - (i_SizeSquare / 2);
        i_PXMalus = Raylib.GetScreenWidth() / 2 + 150 - (i_SizeSquare / 2);
        
        // Then display the text in the square of the Bonus if it's available
        DrawIconPower(powerSystem.GetBonusDisplayed(), "BonusUI", i_PXBonus, i_PY);

        // Then display the text in the square of the Malus if it's available
        DrawIconPower(powerSystem.GetMalusDisplayed(), "MalusUI", i_PXMalus, i_PY);

        // Draw Rectangle for Bonus
        recPower = new(i_PXBonus, i_PY, i_SizeSquare, i_SizeSquare);
        Raylib.DrawRectangleLinesEx(recPower, 3f, UI_Board_Sprite.GetColorCell(5));

        // Draw Rectangle for Malus
        recPower = new(i_PXMalus, i_PY, i_SizeSquare, i_SizeSquare);
        Raylib.DrawRectangleLinesEx(recPower, 3f, UI_Board_Sprite.GetColorCell(6));
        
        // Draw text for Bonus
        DrawTextPower(powerSystem.GetBonusDisplayed(), i_PXBonus, i_PY, UI_Board_Sprite.GetColorCell(5));

        // Draw text for Malus
        DrawTextPower(powerSystem.GetMalusDisplayed(), i_PXMalus, i_PY, UI_Board_Sprite.GetColorCell(6));
    
    }

    private static void DrawIconPower(Power? powerDisplayed, string s_Powertype  , int i_PX, int i_PY)
    {
        if (powerDisplayed != null)
        {
            int i_indexPower = (powerDisplayed.GetTypePower() == TypePower.Bonus) ? (int) powerDisplayed.GetPowerTypeBonus() : (int) powerDisplayed.GetPowerTypeMalus();

            Raylib.DrawTexture(assets.GetTextureFromSet(s_Powertype, i_indexPower), i_PX, i_PY, Color.White);
        }
    }

    public static void DrawTextPower(Power? powerDisplayed, int i_PX, int i_PY, Color colorFont)
    {    
        // Then display the text in the square of the Bonus if it's available
        if (powerDisplayed != null)
        {
            int i_FontSize = 20;

            string s_Text = powerDisplayed.GetNamePower();

            Vector2 v2_TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), s_Text, i_FontSize, 1);
            
            // Put the text of the Power below the square
            i_PY += 115 - (int)v2_TextSize.Y / 2;

            Raylib.DrawText(s_Text, i_PX, i_PY, i_FontSize, colorFont);
        }
    }
}