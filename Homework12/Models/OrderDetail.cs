namespace Homework12.Models
{
    //订单明细类
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }    //商品序号
        public string orderName { get; set; }       //商品名称
        public double orderPrice { get; set; }      //商品价格
        public int orderNum { get; set; }        //商品购买数量
        public int OrderId { get; set; }
    }
}