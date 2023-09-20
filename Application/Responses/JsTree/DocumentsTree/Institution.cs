namespace Application.Responses.JsTree.DocumentsTree
{
    public class Institution
    {
        public string Text { get; set; }
        public int Column { get; set; }
        public List<Year> Children { get; set; }

        public Institution()
        {
            Column = 4;
        }
    }
}
