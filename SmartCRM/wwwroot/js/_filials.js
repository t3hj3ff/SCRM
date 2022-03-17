function InitializeVueFilials(elem, data, saved_filials, confirmFilials) {

    var _data = data;

    var _choosen_filials = saved_filials && saved_filials.length > 0 ? saved_filials : [];

    var choosed = [];
    _data.forEach(function (f, ind) {
        if (_choosen_filials.indexOf(f.ObjectID.toString()) >= 0) {
            choosed.push(f);
        }
    });

    var _id = elem.attr('id') + '-' + 'vue';
    elem.find('.vue-identifier').attr('id', _id);

    _id = '#' + _id;

    var _table;
    var app = new Vue({
        el: _id,
        data: {
            filials: _data,
            choosen_filials: choosed,
            table: {},
        },
        methods: {
            addMerchant: function (filial) {
                this.choosen_filials.push(filial);
            },
            removeMerchant: function (filial) {
                var index = this.choosen_filials.indexOf(filial);
                this.choosen_filials.splice(index, 1);
            },
            chooseAll: function () {
                var _this = this;
                _this.table.rows({ filter: 'applied' }).data().each(function (item, ind) {

                    if (!_this.choosen_filials.some(function (f) { return f.ObjectID == item[0] })) {
                        var _filial = _this.filials.filter(function (_f, ind) {

                            return _f.ObjectID == item[0];
                        });

                        _this.choosen_filials.push(_filial[0]);
                    }
                });
            },
            emptyAll: function () {
                this.choosen_filials = [];
            },
            confirmFilials: function () {
                var merchant_data = [];

                this.choosen_filials.forEach(function (f, ind) {
                    merchant_data.push(f.ObjectID);
                });
                $(this.$el).find('#choosen_filials').val(merchant_data);
                confirmFilials(merchant_data, this.$el.id);

                if ($('.filial-choosed-val.clicked')) {
                    $('.filial-choosed-val.clicked').val(merchant_data.length + ' ფილიალი');
                }

                $(this.$el).find('#filials-modal').modal('hide');
            }
        },
        mounted: function () {
            this.$nextTick(function () {
                var _this = this;
                _this.table = $(this.$el).find('#merch-filials').DataTable({
                    pageLength: 5,
                    "info": false,
                    orderCellsTop: true,
                    fixedHeader: true,
                    "lengthChange": false,
                    "ordering": false,
                    "paging": false,
                    "language": {
                        "search": "ძებნა",
                        "info": "ჩვენება _START_ - _END_ სულ _TOTAL_ ჩანაწერი",
                        paginate: {
                            first: 'პირველი',
                            previous: 'წინა',
                            next: 'შემდეგი',
                            last: 'ბოლო'
                        },
                        "processing": "მონაცემები იტვირთება",
                        "lengthMenu": "აჩვენე _MENU_ ჩანაწერი",
                        "zeroRecords": "ჩანაწერი ვერ მოიძებნა",
                        "emptyTable": "ჩანაწერები ვერ მოიძებნა",
                        "infoEmpty": "ჩანაწერები ვერ მოიძებნა",
                        "infoFiltered": " (სულ _MAX_ ჩანაწერი)"
                    },
                    "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "ყველა"]]
                });

                $(this.$el).find('.head-filter input').each(function (i) {
                    i = i + 1;
                    $(this).on('keyup change', function () {
                        if (_this.table.column(i).search() !== this.value) {
                            _this.table.column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                });

            });
        }
    });

}

