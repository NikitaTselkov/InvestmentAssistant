﻿using DataParserService.Codes;
using DataParserService.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataParserService.DataParser
{
    public class Parser : IParser
    {
        public string ParseCompanyName(SecuritieTQBR securitieTQBR)
        {
            string name = string.Empty;

            try
            {
                if (securitieTQBR == null) throw new ArgumentNullException(nameof(securitieTQBR));

                var web = new HtmlWeb();
                var doc = web.Load($"https://finrange.com/ru/company/MOEX/{securitieTQBR.SECID}");

                name = doc.DocumentNode.SelectSingleNode($"//div[@class='company-brief__title']/h1").InnerText.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> ParseCompanyName Error: {ex.Message}");
            }

            return name;
        }

        public (string sectorLongName, string sectorShortName) ParseCompanySector(SecuritieTQBR securitieTQBR)
        {
            string sectorLongName = string.Empty;
            string sectorShortName = string.Empty;

            try
            {
                if (securitieTQBR == null) throw new ArgumentNullException(nameof(securitieTQBR));

                var web = new HtmlWeb();
                var doc = web.Load($"https://finrange.com/ru/company/MOEX/{securitieTQBR.SECID}");

                sectorLongName = doc.DocumentNode.SelectSingleNode($"/html/body/div[1]/div[1]/section/div/div[1]/div[1]/div/div[1]/div[2]/div[1]/span[1]").InnerText.ToString();
                sectorShortName = doc.DocumentNode.SelectSingleNode($"/html/body/div[1]/div[1]/section/div/div[1]/div[1]/div/div[1]/div[2]/div[1]/span[2]").InnerText.ToString(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> ParseCompanySector Error: {ex.Message}");
            }

            return (sectorLongName, sectorShortName);
        }

        public List<Multiplicator> ParseCompanyAllMultiplicators(Company company)
        {
            var multiplicators = new List<Multiplicator>();

            AddToMultiplicators(ParseCompanyMultiplicator(company, MultiplicatorsCodes.P_E));
            AddToMultiplicators(ParseCompanyMultiplicator(company, MultiplicatorsCodes.P_B));
            AddToMultiplicators(ParseCompanyMultiplicator(company, MultiplicatorsCodes.P_S));
            AddToMultiplicators(ParseCompanyMultiplicator(company, MultiplicatorsCodes.P_BV));
            AddToMultiplicators(ParseCompanyMultiplicator(company, MultiplicatorsCodes.EV_EBITDA));
            AddToMultiplicators(ParseCompanyMultiplicator(company, MultiplicatorsCodes.DEBT_EBITDA));

            return multiplicators;

            void AddToMultiplicators(Multiplicator multiplicator)
            {
                if (multiplicator != null)
                {
                    multiplicators.Add(multiplicator);
                }
            }
        }

        public Multiplicator ParseCompanyMultiplicator(Company company, string multiplicatorName)
        {
            var multiplicator = new Multiplicator { Name = multiplicatorName, CompanyId = company.Id };
            multiplicator.Indexes = new List<Models.Index>();

            try
            {
                if (company == null) throw new ArgumentNullException(nameof(company));

                string timePeriod;
                var web = new HtmlWeb();
                var doc = web.Load($"https://smart-lab.ru/q/{company.SecuritieTQBR.SECID}/f/y/");

                for (int i = 2; i < 9; i++)
                {
                    if (i != 7)
                    {
                        var index = new Models.Index();

                        if (i == 8)
                            timePeriod = "LTM";
                        else
                            timePeriod = doc.DocumentNode.SelectSingleNode($"//td[{i}]").InnerText.ToString();

                        var node = doc.DocumentNode.SelectSingleNode($"//tr[@field='{multiplicatorName}']/td[{i}]").InnerText.ToString();

                        index.Key = timePeriod;

                        if (double.TryParse(Regex.Match(node, @"(?:-)[0-9]{1,3}(?:[.,][0-9]{1,3})?").Value.Replace('.', ','), out double result)) // Находит число с плавающей запятой.
                        {
                            index.Value = result;
                        }

                        multiplicator.Indexes.Add(index);
                    }
                }
            }
            catch (Exception ex) when (ex is FormatException || ex is NullReferenceException) { }
            catch (Exception ex)
            {
                Console.WriteLine($"--> ParseCompanyMultiplicator Error: {ex.Message}");
            }

            return multiplicator.Indexes.Any(a => a != null) ? multiplicator : null;
        }
    }
}
