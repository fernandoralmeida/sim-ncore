namespace Sim.Domain.Cnpj.Extensions;

public static class Tools
{
    public static bool IsNumeric(this string valor)
    {
        int i;
        return int.TryParse(valor, out i);
    }
    public static string Mask(this string value, string mask, char substituteChar = '#')
    {
        int valueIndex = 0;
        try
        {
            return new string(mask.Select(maskChar => maskChar == substituteChar ? value[valueIndex++] : maskChar).ToArray());
        }
        catch (IndexOutOfRangeException e)
        {
            throw new Exception("Valor muito curto para substituir todos os caracteres substitutos na máscara", e);
        }
    }

    public static string MaskRemove(this string valor)
    {
        try
        {
            var str = valor;
            str = new string((from c in str
                              where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                              select c
                    ).ToArray());

            return str;
        }
        catch
        {
            return valor;
        }
    }

    public static string DateDiference(this DateTime date)
    {
        try
        {

            var r = DateTime.Now.Subtract(date).TotalDays / 365;

            if (r < 5)
                return $"0 - 4";
            else if (r >= 5 && r < 10)
                return $"5 - 9";
            else if (r >= 10 && r < 20)
                return $"10 - 19";
            else if (r >= 20 && r < 30)
                return $"20 - 29";
            else if (r >= 30 && r < 40)
                return $"30 - 39";
            else if (r >= 40 && r < 50)
                return $"40 - 49";
            else if (r >= 50)
                return $"50 +";
            else
                return $"...";

        }
        catch
        {
            return date.ToString("yyyy-MM-dd");
        }
    }

}