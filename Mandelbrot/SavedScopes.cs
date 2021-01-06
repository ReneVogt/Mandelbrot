using System.Collections.Generic;
using System.Configuration;

namespace Mandelbrot
{
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public class SavedScopes : List<(string name, double minr, double mini, double maxr, double maxi)>
    {
    }
}
