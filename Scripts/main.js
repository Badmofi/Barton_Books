function sortCustomers(sortByCustomers) {
    window.location.href = "/Customers/All/?sortBy=" + sortByCustomers;
}
function searchCustomers() {
    let searchKey = $('#txtSearch').val();

    window.location.href = "/Customers/All/" + searchKey;
}
function sortLineItems(sortByLineItems) {
    window.location.href = "/InvoiceLineItems/All/?sortBy=" + sortByLineItems;
}
function searchLineItems() {
    let searchKey = $('#txtSearch').val();

    window.location.href = "/InvoiceLineItems/All/" + searchKey;
}
function sortInvoices(sortByInvoices) {
    window.location.href = "/Invoices/All/?sortBy=" + sortByInvoices;
}
function searchInvoices() {
    let searchKey = $('#txtSearch').val();

    window.location.href = "/Invoices/All/" + searchKey;
}
function sortOrderOptions(sortByOrderOptions) {
    window.location.href = "/OrderOptions/All/?sortBy=" + sortByOrderOptions;
}
function sortProducts(sortByProducts) {
    window.location.href = "/Products/All/?sortBy=" + sortByProducts;
}
function searchProducts() {
    let searchKey = $('#txtSearch').val();

    window.location.href = "/Products/All/" + searchKey;
}
function sortStates(sortByStates) {
    window.location.href = "/States/All/?sortBy=" + sortByStates;
}
function searchStates() {
    let searchKey = $('#txtSearch').val();

    window.location.href = "/States/All/" + searchKey;
}