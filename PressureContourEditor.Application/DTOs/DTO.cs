using PressureContourEditor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PressureContourEditor.Application.DTOs
{
    public class RootObject
    {
        [JsonPropertyName("AllowedFamilyNames")]
        public List<AlowwedFamilyWrapperDto> AllowedFamilyNames { get; set; }
    }
    public class AlowwedFamilyWrapperDto
    {
        [JsonPropertyName("FamilyName")]
        public string FamilyName { get; set; }
        [JsonPropertyName("FamilyType")]
        public string FamilyType { get; set; }

        [JsonPropertyName("Parameters")]
        public Parameters Parameters { get; set; }
    }
    public class Parameters
    {
        [JsonPropertyName("ActiveEdge")]
        public List<ContourSideName> ActiveEdge { get; set; }
        [JsonPropertyName("Dimensions")]
        public Dimensions Dimensions { get; set; }

        [JsonPropertyName("DoubleParameters")]
        public List<string> DoubleParameters { get; set; }

        [JsonPropertyName("IntParameters")]
        public List<string> IntParameters { get; set; }

        [JsonPropertyName("ParameterMappings")]
        public Dictionary<string, SideMappingDto> ParameterMappings { get; set; }
    }
    public class Dimensions
    {
        [JsonPropertyName("DoubleParameters")]
        public List<string> DoublrParameters { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }

    }
    public class SideMappingDto
    {
        [JsonPropertyName("Enabled")]
        public string Enabled { get; set; }

        [JsonPropertyName("OffsetStart")]
        public string OffsetStart { get; set; }

        [JsonPropertyName("OffsetEnd")]
        public string OffsetEnd { get; set; }

        [JsonPropertyName("HoleOffset")]
        public string HoleOffset { get; set; }

        [JsonPropertyName("HoleWidth")]
        public string HoleWidth { get; set; }
    }
}
