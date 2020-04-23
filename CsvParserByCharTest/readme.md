# 規則

- 以 Microsoft Excel 儲存的 CSV 格式為主
- 字串內有以下情況，就要以雙引號包住
    - 二個雙引號
    - 逗號
    - 換行符號
- Read() 代表逐個欄位讀取
    - 回傳 string.Empty 代表空字串
    - 回傳 \r\n 代表換行
    - 回傳 null 代表已無資料
    - 所有資料一律回傳字串
    
## Todo

- 回傳 null 時，直接關閉 StreamReader
- Add Property - 總行數
- Add Validation - 欄位數是否一致 (除了最後一個空白行)
- Read() 支援大欄位 模式 - 使用 StringBuilder
