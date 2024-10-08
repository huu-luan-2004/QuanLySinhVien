using QuanLySinhVien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvStudent.CellClick += dgvStudent_CellClick;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

                txtMSSV.Text = row.Cells[0].Value.ToString();
                txtHoTen.Text = row.Cells[1].Value.ToString();
                cmbFaculty.SelectedValue = row.Cells[2].Value;
                txtDTB.Text = row.Cells[3].Value.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB context = new StudentContextDB();
                List<Faculty> listFalcultys = context.Faculties.ToList();
                List<Student> listStudent = context.Students.ToList();
                FillFacultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                StudentContextDB context = new StudentContextDB();
                List<Faculty> listFaculties = context.Faculties.ToList();
                List<Student> listStudents = context.Students.ToList();

                FillFacultyCombobox(listFaculties);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGrid(List<Student> listStudents)
        {
            dgvStudent.Rows.Clear();

            foreach (var item in listStudents)
            {
                int index = dgvStudent.Rows.Add();

                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }
        private void FillFacultyCombobox(List<Faculty> listFaculties)
        {
            this.cmbFaculty.DataSource = listFaculties;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB context = new StudentContextDB();
                long studentID = long.Parse(txtMSSV.Text);
                Student student = context.Students.FirstOrDefault(s => s.StudentID == studentID);

                if (student == null)
                {
                    MessageBox.Show("Không tìm thấy MSSV cần sửa!");
                    return;
                }

                student.FullName = txtHoTen.Text;
                student.FacultyID = (int)cmbFaculty.SelectedValue;
                student.AverageScore = decimal.Parse(txtDTB.Text);
                context.SaveChanges();
                LoadData();
                MessageBox.Show("Cập nhật dữ liệu thành công!");
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB context = new StudentContextDB();

                if (string.IsNullOrEmpty(txtMSSV.Text) || string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtDTB.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                if (txtMSSV.Text.Length != 10)
                {
                    MessageBox.Show("Mã số sinh viên phải có 10 kí tự!");
                    return;
                }
                Student newStudent = new Student()
                {
                    StudentID = long.Parse(txtMSSV.Text),
                    FullName = txtHoTen.Text,
                    FacultyID = (int)cmbFaculty.SelectedValue,
                    AverageScore = decimal.Parse(txtDTB.Text)
                };
                context.Students.Add(newStudent);
                context.SaveChanges();
                LoadData();
                MessageBox.Show("Thêm mới dữ liệu thành công!");
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB context = new StudentContextDB();

                long studentID = long.Parse(txtMSSV.Text);
                Student student = context.Students.FirstOrDefault(s => s.StudentID == studentID);

                if (student == null)
                {
                    MessageBox.Show("Không tìm thấy MSSV cần xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Cảnh báo", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Xóa sinh viên thành công!");
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ResetForm()
        {
            txtMSSV.Clear();
            txtHoTen.Clear();
            txtDTB.Clear();
            cmbFaculty.SelectedIndex = 0;
        }
    }
}
