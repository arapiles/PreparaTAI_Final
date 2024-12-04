// SIN USAR
namespace WebApiProyecto.Extensions // Evita conflictos de nombre y obliga a usar using 
{

    public static class ListExtensions
    {
        // Mwtodo calcula la racha más larga de "true" consecutivos
        public static int GetRachaMasLarga(this List<bool> results)
        {
            int rachaActual = 0;
            int rachaMaxima = 0; // Racha más larga encontrada

            foreach (var result in results)
            {
                if (result)  // Si es true (acierto)
                {
                    rachaActual++;  // Aumenta la racha actual
                    rachaMaxima = Math.Max(rachaMaxima, rachaActual);  // Actualiza la racha máxima si es necesario
                }
                else  // Si es false (fallo)
                {
                    rachaActual = 0;  // Reinicia la racha actual
                }
            }

            return rachaMaxima;
        }
        /* OTROS METODOS A PROBAR */
        /*
         // Obtener la racha actual
            public static int GetCurrentStreak(this List<bool> results)
            {
                int streak = 0;
                for (int i = results.Count - 1; i >= 0; i--)
                {
                    if (results[i])
                        streak++;
                    else
                        break;
                }
                return streak;
            }

            // Calcular el porcentaje de aciertos
            public static double GetSuccessRate(this List<bool> results)
            {
                if (!results.Any()) return 0;
                return Math.Round((double)results.Count(r => r) / results.Count * 100, 2);
            }

            // Obtener resumen de resultados
            public static (int total, int aciertos, int fallos, double porcentaje) GetSummary(this List<bool> results)
            {
                int total = results.Count;
                int aciertos = results.Count(r => r);
                int fallos = total - aciertos;
                double porcentaje = total == 0 ? 0 : Math.Round((double)aciertos / total * 100, 2);

                return (total, aciertos, fallos, porcentaje);
            }

            // Obtener tendencia (mejorando, empeorando, estable)
            public static string GetTrend(this List<bool> results, int windowSize = 5)
            {
                if (results.Count < windowSize * 2) return "Insuficientes datos";

                var recentResults = results.Skip(results.Count - windowSize).Take(windowSize);
                var previousResults = results.Skip(results.Count - (windowSize * 2)).Take(windowSize);

                double recentSuccess = recentResults.Count(r => r) / (double)windowSize;
                double previousSuccess = previousResults.Count(r => r) / (double)windowSize;

                if (recentSuccess > previousSuccess + 0.1) return "Mejorando";
                if (recentSuccess < previousSuccess - 0.1) return "Empeorando";
                return "Estable";
            }

            // Obtener días consecutivos con actividad
            public static int GetConsecutiveDays<T>(this List<T> items, Func<T, DateTime> dateSelector)
            {
                if (!items.Any()) return 0;

                var dates = items.Select(dateSelector)
                               .Select(d => d.Date)
                               .Distinct()
                               .OrderBy(d => d)
                               .ToList();

                int maxDays = 1;
                int currentDays = 1;

                for (int i = 1; i < dates.Count; i++)
                {
                    if (dates[i] == dates[i - 1].AddDays(1))
                    {
                        currentDays++;
                        maxDays = Math.Max(maxDays, currentDays);
                    }
                    else
                    {
                        currentDays = 1;
                    }
                }

                return maxDays;
            }
         */
    }
}