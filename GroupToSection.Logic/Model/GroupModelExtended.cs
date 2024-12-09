namespace GroupToSection.Logic.Model
{
    public partial class Group
    {
        public string GetIdentifier() => $"{Course_Id}:{Id}";
    }
} 