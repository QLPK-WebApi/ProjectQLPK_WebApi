using System.IO.Pipelines;
using Microsoft.VisualBasic;

namespace App_QLPK.Application.Common;


/// <summary>
///     Phân trang dùng chung cho tất cả API danh sách.
/// </summary>
public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>(); // nếu chưa có thì là mảng rỗng

    /*
        IReadOnlyList : chỉ đọc, không cho Add, Remove ,...

        <T> dùng cho mọi kiểu dữ liệu : Patient , Doctor , Appointment, ...

        init : chỉ được gán giá trị khi khởi tạo , sau đó không sửa được
        
    */

    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }

    public int TotalPages => PageSize <= 0 ? 0 :
    (int)Math.Ceiling(TotalCount / (double)PageSize);
    /*
        tính tổng số trang cần có để chứa dữ liệu (nếu PageSize <= 0 thì trả về 0 , ngược lại -> chạy phần phía sau)

        (int)Math.Ceiling : làm tròn lên
    */
    public bool HasPrevious => PageIndex > 1 ;  // điều kiện để có trang trước
    public bool HasNext => PageIndex < TotalPages; // điều kiện để có trang tiếp theo

    
}

