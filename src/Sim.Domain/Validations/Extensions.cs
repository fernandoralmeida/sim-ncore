using System.Globalization;
using System.Text;

namespace Sim.Domain.Validations;

public static class Extensions {
    public static string NormalizeText(this string text) {
            return new string(text
                    .Normalize(NormalizationForm.FormD)
                    .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                    .ToArray());
    }
}