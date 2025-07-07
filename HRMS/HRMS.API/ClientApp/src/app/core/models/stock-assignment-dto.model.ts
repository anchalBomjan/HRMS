export interface IStockAssignmentDTO {
    id: number;
    employeeName: string;
    stockName: string;
    stockType: string;             // ✅ Add this line
    assignedQuantity: number;
    assignmentDate: string;
  }