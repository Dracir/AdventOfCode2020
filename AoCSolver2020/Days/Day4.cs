using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Day4 : DayBase
{

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}

	//-----------------------------------------------------------------

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public struct Passport
	{
		public Dictionary<string, string> Entries;
		public int? BirthYear;
		public int? IssueYear;
		public int? ExpirationYear;
		public int? Height;
		public string HeightUnit;
		public string HairColor;
		public string EyeColor;
		public string PassportID;
		public string CountryID;

		public Passport(Dictionary<string, string> entries)
		{
			Entries = entries;
			BirthYear = null;
			IssueYear = null;
			ExpirationYear = null;
			Height = null;
			HeightUnit = null;
			HairColor = null;
			EyeColor = null;
			PassportID = null;
			CountryID = null;
		}
	}

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var passports = ParsePassports(input);
		var entriesRequired = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", /*"cid"*/ };

		var nbValid = 0;

		foreach (var item in passports)
		{
			if (entriesRequired.All(entryRequired => item.Entries.ContainsKey(entryRequired)))
				nbValid++;
		}

		return nbValid;
	}

	private static List<Passport> ParsePassports(string input)
	{
		var passportsInput = input.Replace('\r', '\n').Split("\n\n");
		var passports = new List<Passport>();
		foreach (var passportLine in passportsInput)
		{
			Passport passport = ParsePassportLine(passportLine);
			passports.Add(passport);
		}
		return passports;
	}

	private static Passport ParsePassportLine(string passportLine)
	{
		passportLine = passportLine.Replace('\n', ' ').Trim();

		var entries = new Dictionary<string, string>();
		var passport = new Passport(entries);

		foreach (var item in passportLine.Split(" "))
		{
			var values = item.Split(':');
			entries[values[0]] = values[1];
			switch (values[0])
			{
				case "byr":
					if (Int32.TryParse(values[1], out var byr))
						passport.BirthYear = byr;
					break;
				case "iyr":
					if (Int32.TryParse(values[1], out var iyr))
						passport.IssueYear = iyr;
					break;
				case "eyr":
					if (Int32.TryParse(values[1], out var eyr))
						passport.ExpirationYear = eyr;
					break;
				case "hgt":
					passport.Height = new Regex("(\\d*)").Match(values[1]).Groups.IntValue(1);
					passport.HeightUnit = new Regex("\\d*([a-z]*)").Match(values[1]).Groups[1].Value;
					break;
				case "hcl":
					passport.HairColor = values[1];
					break;
				case "ecl":
					passport.EyeColor = values[1];
					break;
				case "pid":
					if (Int32.TryParse(values[1], out var pid))
						passport.PassportID = values[1];
					break;
				case "cid":
					if (Int32.TryParse(values[1], out var cid))
						passport.CountryID = values[1];
					break;
			}
		}

		return passport;
	}

	//-----------------------------------------------------------------
	// 67 wrong
	public override long Part2(string input)
	{
		//var test = ParsePassportLine("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd\nbyr:1937 iyr:2017 cid:147 hgt:183cm");
		var passports = ParsePassports(input);

		var validPassport = passports.Where(passport => IsValidPassport(passport));

		return validPassport.Count();
	}

	public bool IsValidPassport(Passport item)
	{
		if (item.BirthYear == null || item.IssueYear == null || item.ExpirationYear == null
			|| item.HeightUnit == null || item.Height == null
			|| item.HairColor == null || item.EyeColor == null || item.PassportID == null)
		{
			return false;
		}
		else
		{
			if (!item.BirthYear.Value.Between(1920, 2002)) return false;
			if (!item.IssueYear.Value.Between(2010, 2020)) return false;
			if (!item.ExpirationYear.Value.Between(2020, 2030)) return false;
			if (item.HeightUnit == "in")
			{
				if (!item.Height.Value.Between(59, 76)) return false;
			}
			else if (item.HeightUnit == "cm")
			{
				if (!item.Height.Value.Between(150, 193)) return false;
			}
			else
				return false;
			if (!Regex.IsMatch(item.HairColor, "#[0-9a-f]{6}")) return false;
			if (!Regex.IsMatch(item.EyeColor, "(amb|blu|brn|gry|grn|hzl|oth)")) return false;
			if (!Regex.IsMatch(item.PassportID, "[0-9]{9}")) return false;
		}

		return true;
	}

}
