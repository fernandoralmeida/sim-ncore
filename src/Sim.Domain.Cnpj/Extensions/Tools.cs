namespace Sim.Domain.Cnpj.Extensions;

public static class Tools {
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
}