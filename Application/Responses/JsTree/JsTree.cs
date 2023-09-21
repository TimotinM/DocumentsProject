namespace Application.Responses.JsTree
{
    public class JsTree
    {
        public string Text { get; set; }
        public int? Column { get; set; }
        public string? Icon { get; set; }
        public int? Value { get; set; }
        public List<JsTree>? Children { get; set; }
    }
}
