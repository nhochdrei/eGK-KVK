﻿using System;
using System.Xml.Serialization;

namespace CardReader.Egk.GeschuetzteVersichertendaten
{
	[Serializable]
	[XmlType(AnonymousType = true)]
	[XmlRoot("UC_GeschuetzteVersichertendatenXML",IsNullable = false)]
	public class GeschuetzteVersichertendaten
	{
		/// <summary>
		/// Gibt an, ob für den Versicherten eine Befreiung nach § 62 SGB V vorliegt.
		/// Dieses Datenfeld ist besonders schützenswert und daher nicht frei auslesbar, sondern nur berechtigten Personen zugänglich.
		/// </summary>
		[XmlElement("Zuzahlungsstatus")]
		public Zuzahlungsstatus Zuzahlungsstatus { get; set; }


		/// <summary> Gibt die Zugehörigkeit des Versicherten zu einer besonderen Personengruppe an. (nur v5.1)</summary>
		[XmlElement("Besondere_Personengruppe")]
		public string BesonderePersonengruppe51 { get; set; }

		/// <summary> Gibt die Zugehörigkeit des Versicherten zu einer besonderen Personengruppe an. (nur v5.2)</summary>
		[XmlElement("BesonderePersonengruppe",DataType = "integer")]
		public string BesonderePersonengruppe52 { get; set; }


		/// <summary> 
		/// Gibt die Zugehoerigkeit des Versicherten zu einer besonderen Personengruppe an. Die Kennzeichnung erfolgt gemaess der Schluesseltabelle.
		/// 4 = BSHG(Bundessozialhilfegesetz) § 264 SGB V,
		/// 6 = BVG(Gesetz über die Versorgung der Opfer des Krieges), 
		/// 7 = SVA-Kennzeichnung für zwischenstaatliches Krankenversicherungsrecht: - Personen mit Wohnsitz im
		/// Inland, Abrechnung nach Aufwand, 
		/// 8 = SVA-Kennzeichnung, pauschal,
		/// 9 = Empfänger von Gesundheitsleistungen nach §§ 4 und 6 des Asylbewerberleistungsgesetzes(AsylbLG). 
		/// (nur v5.2)
		/// </summary>
		public string BesonderePersonengruppe => this.BesonderePersonengruppe51 ?? this.BesonderePersonengruppe52;

		/// <summary> Gibt die Teilnahme des Versicherten an einem Disease Management Program an. Die Kennzeichnung erfolgt gemäß der Schlüsseltabelle. </summary>
		[XmlElement("DMP_Kennzeichnung")]
		public string DMP_Kennzeichnung { get; set; }

		/// <summary> 
		/// Gibt an, ob fuer den Versicherten ein aerztlicher Selektivvertrag vorliegt.
		/// Dieses Datenfeld ist besonders schuetzenswert und daher nicht frei auslesbar, sondern nur berechtigten Personen zugaenglich.
		/// Schluesselverzeichnis:
		/// 1 = aerztlicher Selektivvertrag liegt vor
		/// 0 = aerztlicher Selektivvertrag liegt nicht vor
		/// 9 = aerztliches Selektivvertragskennzeichen wird nicht genutzt
		/// (nur v5.2)
		/// </summary>
		[XmlElement("Selektivvertraege")]
		public Selektivvertraege Selektivvertraege { get; set; }

		/// <summary> 
		/// Dieses Feld dient ausschließlich zur Angabe des ruhenden Leistungsanpruchs nach § 16 Abs. 3a und § 16 Abs. 1 bis 3 SGB V.
		/// Mögliche Ausprägungen des ruhenden Leistungsanspruchs sind:
		/// 1 = vollständig
		/// 2 = eingeschränkt
		/// (nur v5.2)
		/// </summary>
		[XmlElement("RuhenderLeistungsanspruch")]
		public RuhenderLeistungsanspruch RuhenderLeistungsanspruch { get; set; }

		[XmlAttribute("CDM_VERSION")]
		public string CDM_VERSION { get; set; }
	}



	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Zuzahlungsstatus
	{
		/// <summary>
		/// Gibt an, ob für den Versicherten eine Befreiung nach § 62 SGB V vorliegt.
		/// Dieses Datenfeld ist besonders schützenswert und daher nicht frei auslesbar, sondern nur berechtigten Personen zugänglich.
		/// </summary>
		[XmlElement("Status",DataType = "integer")]
		public string Status { get; set; }

		/// <summary>
		/// Gibt die Gültigkeit der Befreiung von der Zuzahlungspflicht nach § 62 SGB V an. Format: YYYYMMDD
		/// Dieses Datenfeld ist besonders schützenswert und daher nicht frei auslesbar, sondern nur berechtigten Personen zugänglich.
		/// </summary>
		[XmlElement("Gueltig_bis")]
		public string Gueltig_bis { get; set; }
	}


	/// <summary> (nur v5.2) </summary>
	[Serializable]
	[XmlType(AnonymousType = true)]
	public class Selektivvertraege
	{
		/// <summary> 
		/// Gibt an, ob fuer den Versicherten ein aerztlicher Selektivvertrag vorliegt.
		/// Dieses Datenfeld ist besonders schuetzenswert und daher nicht frei auslesbar, sondern nur berechtigten Personen zugaenglich.
		/// Schluesselverzeichnis:
		/// 1 = aerztlicher Selektivvertrag liegt vor
		/// 0 = aerztlicher Selektivvertrag liegt nicht vor
		/// 9 = aerztliches Selektivvertragskennzeichen wird nicht genutzt
		/// </summary>
		[XmlElement("Aerztlich",DataType = "integer")]
		public string Aerztlich { get; set; }

		/// <summary> 
		/// Gibt an, ob fuer den Versicherten ein zahnaerztlicher Selektivvertrag vorliegt.
		/// Dieses Datenfeld ist besonders schuetzenswert und daher nicht frei auslesbar, sondern nur berechtigten Personen zugaenglich.
		/// Schluesselverzeichnis:
		/// 1 = zahnaerztlicher Selektivvertrag liegt vor
		/// 0 = zahnaerztlicher Selektivvertrag liegt nicht vor
		/// 9 = zahnaerztliches Selektivvertragskennzeichen wird nicht genutzt
		/// </summary>
		[XmlElement("Zahnaerztlich",DataType = "integer")]
		public string Zahnaerztlich { get; set; }

		/// <summary> 
		/// Gibt die Paragraphen des  SGB V an, in denen Selektivvertraege beschrieben sind.
		/// Die Angaben gelten fuer folgende Selektivvertraege:
		/// 1. Stelle: Hausarztzentrierte Versorgung(§73b)
		/// 2. Stelle: Besondere ambulante aerztliche Versorgung(§73c)
		/// 3. Stelle: Strukturierte Behandlungsprogramme(§137f)
		/// 4. Stelle: Integrierte Versorgung(§140a)
		/// Die Stellen 1 und 3 koennen den Wert = 1 (true) nur annehmen, wenn Aerztlich = 1 (true) ist.
		/// 
		/// Die Stellen 2 und 4 koennen sowohl zur Kennzeichnung aerztlicher als auch zahnaerztlicher Selektivvertraege genutzt werden.
		/// Beispiel: 1000
		/// Es liegt ein Selektivvertrag fuer die Hausarztzentrierte Versorgung nach §73b vor.
		/// In der Testphase koennen die Krankenkassen im geschuetzten Bereich die Paragraphen des SGB V,in denen Selektivvertraege beschrieben sind (§§73b, 73c, 137f, 140a), im Rahmen der offenen Speicherkapazitaet abbilden. 
		/// </summary>
		[XmlElement("Art")]
		public string Art { get; set; }
	}


	/// <summary> (nur v5.2) </summary>
	[Serializable]
	[XmlType(AnonymousType = true)]
	public class RuhenderLeistungsanspruch
	{
		/// <summary>
		/// Gibt den Beginn des Versicherungsschutzes (hier: Leistungsanspruch) des Versicherten bei dem unter Klasse Kostenträger angegebenen Kostenträger an. 
		/// Format: YYYYMMDD (ISO-8601)
		/// </summary>
		[XmlElement("Beginn")]
		public string Beginn { get; set; }

		/// <summary>
		/// Gibt das Ende des Versicherungsschutzes (hier: Leistungsanspruch) des Ver-sicherten bei dem unter Klasse Kostenträger angegebenen Kostenträger an. 
		/// Format: YYYYMMDD (ISO-8601)
		/// </summary>
		[XmlElement("Ende")]
		public string Ende { get; set; }

		/// <summary> Dieses Feld dient ausschließlich zur Angabe des ruhenden Leistungsanpruchs nach § 16 Abs. 3a und § 16 Abs. 1 bis 3 SGB V.
		/// Mögliche Ausprägungen des ruhenden Leistungsanspruchs sind:
		/// 1 = vollständig
		/// 2 = eingeschränkt 
		/// </summary>
		[XmlElement("ArtDesRuhens",DataType = "integer")]
		public string ArtDesRuhens { get; set; }
	}
}