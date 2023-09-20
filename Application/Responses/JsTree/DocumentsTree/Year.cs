namespace Application.Responses.JsTree.DocumentsTree
{
    public class Year
    {
        public string Text { get; set; }
        public int Column { get; set; }
        public List<MacroType> Children { get; set; }

        public Year()
        {
            Column = 3;
        }

    }
}
