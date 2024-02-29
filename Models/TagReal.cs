

namespace WpfApp1
{
    public class TagReal
    {


        public string datetime { get; set; }
        public string value { get; set; }
        public string wkno { get; set; }

        public int tag_id { get; set; }

        public TagReal(string dateTime, string value, string wkno, int tag_id)
        {
            datetime = dateTime;
            this.value = value;
            this.wkno = wkno;
            this.tag_id = tag_id;



        }
        public TagReal(string dateTime, string value, int tag_id)
        {
            datetime = dateTime;
            this.value = value;
            this.tag_id = tag_id;
        }
        public TagReal()
        {

        }

    }
}
