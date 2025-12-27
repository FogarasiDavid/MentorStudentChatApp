using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }
        public Email(string value) 
        {
            Value = value;
        }
        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Az email nem lehet üres");
            }
            if (!value.Contains("@"))
            {
                throw new ArgumentException("Hibás email forma");
            }
            return new Email(value.Trim().ToLower());
        }
        public override bool Equals(object? obj) => obj is Email other && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value;
    }
}
