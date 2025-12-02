$(document).ready(function () {
    loadShipments(1); // أول صفحة
});

// يحدد الكنترولر المناسب حسب الـ URL (هوم ولا أدمن)
function getControllerPath() {
    const path = window.location.pathname.toLowerCase();

    // لو جوه الادمن (/admin/...)
    if (path.startsWith("/admin/")) {
        return "/Admin/Shipments";   // كنترولر الأدمن
    }

    // لو برا الادمن (الصفحة الرئيسية للمستخدم)
    return "/Shipment";              // كنترولر الهوم
}

function loadShipments(page) {
    GetShipments(page, function (response) {

        console.log("📦 Response from API:", response);

        // لو الـ API بيرجع PageResult:
        var pageResult = response.Data ?? response.data;
        var items = pageResult.Data ?? pageResult.data;

        console.log("✅ pageResult:", pageResult);
        console.log("✅ items passed to renderShipments:", items, "Array?", Array.isArray(items));

        renderShipments(items);              // عرض الصفوف
        renderPagination(pageResult, page);  // عرض أزرار الباجينيشن

    }, function (xhr) {
        window.alert("حصلت مشكلة في تحميل البيانات!");
    });
}

function GetShipments(pageNumber, onSuccess, onError) {
    ApiClient.get(`/api/Shipments?page=${pageNumber}`, onSuccess, onError, true);
}

function formatDate(dateStr) {
    let date = new Date(dateStr);
    return date.toLocaleDateString('en-GB');
}

function formatCurrency(val) {
    let formatted = Number(val).toLocaleString('en-EG', { style: 'currency', currency: 'EGP' });
    return formatted.replace(/[^\d.,EGP]/g, '');
}

function renderShipments(data) {
    try {
        console.log("renderShipments data:", data);

        if (!Array.isArray(data)) {
            console.error("❌ renderShipments expected Array but got:", data);
            return;
        }

        console.log("Array? true length:", data.length);

        var tbody = $("#shipments-table-body");
        tbody.empty();

        data.forEach(function (shipment, index) {
            var row = `
                <tr>
                    <td>${index + 1}</td>
                    <td>${shipment.TrackingNumber ?? ''}</td>
                    <td dir="ltr">${formatDate(shipment.ShipingDate)}</td>
                    <td>${formatCurrency(shipment.ShipingRate)}</td>
                    <td>${shipment.SenderData?.SenderName ?? ''}</td>
                    <td>${shipment.ReciverData?.ReceiverName ?? ''}</td>
                    <td>
                        <a href="#" class="view-shipment" data-id="${shipment.Id}" title="View">
                            <i class="fa fa-eye" aria-hidden="true"></i>
                        </a>
                        <a href="#" class="edit-shipment" data-id="${shipment.Id}" title="Edit" style="margin-left:10px;">
                            <i class="fa fa-edit" aria-hidden="true"></i>
                        </a>
                        <a href="#" class="delete-shipment" data-id="${shipment.Id}" title="Delete" style="margin-left:10px;">
                            <i class="fa fa-square"></i>
                        </a>
                    </td>      
                </tr>
            `;
            tbody.append(row);
        });
    } catch (err) {
        console.error("❌ Error inside renderShipments:", err);
        console.error(err.stack);
    }
}

function renderPagination(pageResult, currentPage) {
    var paginationDiv = $("#pagination");
    paginationDiv.empty();

    var totalPages = pageResult.totalPages ?? pageResult.TotalPages;
    var hasNext = pageResult.hasNext ?? pageResult.HasNext;
    var hasPrevious = pageResult.hasPrevious ?? pageResult.HasPrevious;

    if (totalPages <= 1) {
        return; // لو مفيش صفحات تانية متعرضش الأزرار
    }

    var html = `<div class="pagination-buttons">`;

    // Previous button
    html += `<button class="page-btn ${!hasPrevious ? 'disabled' : ''}" 
                onclick="loadShipments(${currentPage - 1})" ${!hasPrevious ? 'disabled' : ''}>
                ‹ Previous
             </button>`;

    // Page numbers
    for (let i = 1; i <= totalPages; i++) {
        html += `<button class="page-btn ${i === currentPage ? 'active' : ''}" 
                    onclick="loadShipments(${i})">${i}</button>`;
    }

    // Next button
    html += `<button class="page-btn ${!hasNext ? 'disabled' : ''}" 
                onclick="loadShipments(${currentPage + 1})" ${!hasNext ? 'disabled' : ''}>
                Next ›
             </button>`;

    html += `</div>`;
    paginationDiv.html(html);
}

// ========================
//  Shipment Button Clicks
// ========================
$(document).on("click", ".view-shipment", function (e) {
    e.preventDefault();

    const shipmentId = $(this).data("id");
    if (!shipmentId) {
        console.error("❌ No shipment ID found!");
        return;
    }

    const path = getControllerPath();
    window.location.href = `${path}/Show?id=${shipmentId}`;
});

$(document).on("click", ".edit-shipment", function (e) {
    e.preventDefault();

    const shipmentId = $(this).data("id");
    if (!shipmentId) {
        console.error("❌ No shipment ID found!");
        return;
    }

    const path = getControllerPath();
    window.location.href = `${path}/Edit?id=${shipmentId}`;
});

$(document).on("click", ".delete-shipment", function (e) {
    e.preventDefault();

    const shipmentId = $(this).data("id");
    if (!shipmentId) {
        console.error("❌  delete ");
        return;
    }

    const path = getControllerPath();
    window.location.href = `${path}/Delete?id=${shipmentId}`;
});
