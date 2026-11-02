using GameServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet<User> => User 데이터를 조회하거나 추가, 삭제하는 작업에 사용하는 객체의 타입
    // Set<User>() => DbContext에 정의된 메서드, User 데이터들을 관리할 수 있는 DbSet<User> 객체를 반환
    public DbSet<User> Users => Set<User>();


    // modelBuilder => 테이블·컬럼·인덱스 등의 매핑 규칙을 설정하는 객체
    // Entity<User>() => 엔티티의 매핑 규칙을 설정할 데이터를 User로 정함
    // HasIndex(u => u.Username) => Username에 검색을 효율적으로 수행하도록 돕는 인덱스를 설정
    // IsUnique() => Username 인덱스를 고유 인덱스로 설정해서, DB에 적용되면 같은 Username의 중복 저장을 막음
    // OnModelCreating은 EF Core가 모델을 구성할 때 호출되는 메서드로, 엔티티의 속성, 관계, 인덱스 등을 정의할 수 있다.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Username은 로그인 아이디이므로 중복 불가(고유 인덱스).
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}
