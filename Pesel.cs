using System.Globalization;

namespace PeselApp
{
    public class Pesel
    {

        private string pesel;
        private string sex;
        public Pesel(string pesel, string sex)
        {
            this.pesel = pesel;
            this.sex = sex;
        }

        public bool IsValid()
        {
            if (!IsELevenCHar())
            {
                throw new Exception("Błąd: [PESEL nie zawiera 11 znaków]");
            }
            if (!IsSexValid())
            {
                throw new Exception("Błąd: [Nie podano prawidłowego znaku dla płci]");
            }
            if (!IsEveryCharDigit())
            {
                throw new Exception("Błąd: [PESEL nie zawiera tylko cyfr 0-9]");
            }
            if (!IsCheckSumValid())
            {
                throw new Exception("Błąd: [Nieprawidłowa suma kontrolna]");
            }
            if (!IsBirthDateValid())
            {
                throw new Exception("Błąd: [Nieprawidłowa data urodzenia w numerze PESEL]");
            }
            if (IsSexInPeselValid())
            {
                throw new Exception("Błąd: [Nieprawidłowa liczba określająca płeć w numerze PESEL]");
            }
            return true;
        }

        private bool IsELevenCHar()
        {
            return this.pesel.Length == 11;
        }

        private bool IsSexValid()
        {
            if ((sex == "K") || (sex == "k") || (sex == "M") || (sex == "m"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsEveryCharDigit()
        {
            foreach (char ch in this.pesel)
            {
                if (!(ch >= '0' && ch <= '9'))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsCheckSumValid()
        {
            var sum = (pesel[0] - 48) + ((pesel[1] - 48) * 3) % 10 + ((pesel[2] - 48) * 7) % 10 + ((pesel[3] - 48) * 9) % 10 + (pesel[4] - 48) + ((pesel[5] - 48) * 3) % 10
                       + ((pesel[6] - 48) * 7) % 10 + ((pesel[7] - 48) * 9) % 10 + (pesel[8] - 48) + ((pesel[9] - 48) * 3) % 10;

            if ((pesel[10] - 48) == (10 - sum % 10))
            {
                return true;
            }
            return false;
        }

        private bool IsBirthDateValid()
        {
            var monthString = pesel.Substring(2, 2);
            var monthpesel = int.Parse(monthString);
            var yearbase = 0;
            int month = 0;
            DateOnly birthDate;
            switch (monthpesel)
            {
                case var nmonth when nmonth >= 81 && nmonth <= 92:
                    yearbase = 1800;
                    month = monthpesel - 80;
                    break;
                case var nmonth when nmonth >= 1 && nmonth <= 12:
                    yearbase = 1900;
                    month = monthpesel;
                    break;
                case var nmonth when nmonth >= 21 && nmonth <= 32:
                    yearbase = 2000;
                    month = monthpesel - 20;
                    break;
                case var nmonth when nmonth >= 41 && nmonth <= 52:
                    yearbase = 2100;
                    month = monthpesel - 40;
                    break;
                case var nmonth when nmonth >= 61 && nmonth <= 72:
                    month = monthpesel - 60;
                    yearbase = 2200;
                    break;
            }
            if (yearbase == 0)
            {
                return false;
            }

            var year = yearbase + int.Parse(pesel.Substring(0, 2));
            var monthStr = month.ToString("d2");
            var day = pesel.Substring(4, 2);

            if (DateOnly.TryParseExact($"{day}.{monthStr}.{year}", "d", CultureInfo.CurrentCulture, 0, out birthDate))
            {
                return true;
            }
            return false;

        }

        private bool IsSexInPeselValid()
        {
            if (((sex == "K") || (sex == "k")) && ((pesel[10] - 40) % 2 == 0))
            {
                return true;
            }
            else if (((sex == "M") || (sex == "m")) && !((pesel[10] - 40) % 2 == 0))
            {
                return true;
            }
            return false;
        }
    }
}
