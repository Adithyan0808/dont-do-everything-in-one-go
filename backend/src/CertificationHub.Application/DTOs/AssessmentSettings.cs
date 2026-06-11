namespace CertificationHub.Application.DTOs;

public sealed class AssessmentSettings
{
    public IList<AssessmentPassMark> PassMarks { get; set; } = new List<AssessmentPassMark>
    {
        new() { Vendor = "AWS", Certification = "AWS Certified Solutions Architect - Associate", PassingScore = 720 },
        new() { Vendor = "Azure", Certification = "Azure Fundamentals", PassingScore = 700 },
        new() { Vendor = "GCP", Certification = "Google Cloud Digital Leader", PassingScore = 700 }
    };
}

public sealed class AssessmentPassMark
{
    public string Vendor { get; set; } = string.Empty;

    public string Certification { get; set; } = string.Empty;

    public int PassingScore { get; set; }
}
