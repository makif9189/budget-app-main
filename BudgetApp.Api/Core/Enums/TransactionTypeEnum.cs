namespace BudgetApp.Api.Core.Enums
{
    /// <summary>
    /// Defines the specific types of financial transactions within the system.
    /// This corresponds to the 'transaction_type_enum' in the database.
    /// </summary>
    public enum TransactionTypeEnum
    {
        /// <summary>
        /// A general spending or purchase.
        /// </summary>
        HARCAMA,

        /// <summary>
        /// An automatic payment for a pre-defined installment.
        /// </summary>
        OTOMATIK_TAKSIT,

        /// <summary>
        /// A payment made to a credit card or to settle a debt.
        /// </summary>
        ODEME,

        /// <summary>
        /// Interest charge, typically on a credit card balance.
        /// </summary>
        FAIZ,

        /// <summary>
        /// A refund received for a previous expense.
        /// </summary>
        IADE,

        /// <summary>
        /// Carried-over interest from a previous billing cycle.
        /// </summary>
        DEVIR_FAIZI,

        /// <summary>
        /// Carried-over KKDF (Resource Utilization Support Fund) tax.
        /// </summary>
        DEVIR_KKDF,

        /// <summary>
        /// Carried-over BSMV (Banking and Insurance Transaction Tax).
        /// </summary>
        DEVIR_BSMV,

        /// <summary>
        /// Any form of income received.
        /// </summary>
        GELIR,

        /// <summary>
        /// A general expense not tied to a specific category, like a bank fee.
        /// </summary>
        GENEL_GIDER
    }
}
